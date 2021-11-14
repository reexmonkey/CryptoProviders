using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions;
using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public abstract class SymmetricEncrypter : ISymmetricEncrypter, IDisposable
    {
        public const int DefaultIterations = 10000;

        private readonly int iterations;
        private readonly SymmetricAlgorithm algorithm;
        private bool disposedValue;

        protected CipherMode CipherMode { get; }
        protected PaddingMode PaddingMode { get; }

        protected SymmetricEncrypter(
            SymmetricAlgorithm algorithm,
            int iterations,
            CipherMode cipherMode,
            PaddingMode paddingMode)
        {
            this.algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            this.iterations = iterations;
            CipherMode = cipherMode;
            PaddingMode = paddingMode;
        }

        private byte[] Encrypt(byte[] data, byte[] salt, byte[] iv, byte[] key)
        {
            algorithm.Mode = CipherMode;
            algorithm.Padding = PaddingMode;
            using var enc = algorithm.CreateEncryptor(key, iv);
            using var mstream = new MemoryStream();
            using var cstream = new CryptoStream(mstream, enc, CryptoStreamMode.Write);
            cstream.Write(data, 0, data.Length);
            cstream.FlushFinalBlock();

            //add salt to iteration vector and data in memory stream
            var cipher = salt.Concat(iv).Concat(mstream.ToArray()).ToArray();
            return cipher;
        }

        public byte[] Encrypt<T>(T data, Func<T, byte[]> serialize, byte[] key)
            => Encrypt(serialize(data), key);

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            var blocks = algorithm.BlockSize / 8;
            var keysize = algorithm.KeySize / 8;
            var salt = new byte[blocks];
            var iv = new byte[blocks];

            CryptoProviderExtensions.FillWithRandomBytes(salt);
            CryptoProviderExtensions.FillWithRandomBytes(iv);

            using var derived = new Rfc2898DeriveBytes(key, salt, iterations);
            var password = derived.GetBytes(keysize);
            return Encrypt(data, salt, iv, password);
        }

        private (byte[] plain, int count) Decrypt(byte[] cipher, byte[] iv, byte[] key)
        {
            algorithm.Mode = CipherMode;
            algorithm.Padding = PaddingMode;
            using var transform = algorithm.CreateDecryptor(key, iv);
            using var mstream = new MemoryStream(cipher);
            using var cstream = new CryptoStream(mstream, transform, CryptoStreamMode.Read);
            var buffer = new byte[cipher.Length];
            var pending = cipher.Length;
            var received = 0;
            do
            {
                var read = cstream.Read(buffer, received, pending);
                if (read == 0) break;
                received += read;
                pending -= read;
            } while (pending > 0);

            return (buffer, received);
        }

        public T Decrypt<T>(byte[] cipher, Func<byte[], T> deserialize, byte[] key)
        {
            var blocks = algorithm.BlockSize / 8;
            var keysize = algorithm.KeySize / 8;
            var salt = cipher.Take(blocks).ToArray();
            var iv = cipher.Skip(blocks).Take(blocks).ToArray();
            var payload = cipher.Skip(2 * blocks).Take(cipher.Length - (2 * blocks)).ToArray();

            using var derived = new Rfc2898DeriveBytes(key, salt, iterations);
            var password = derived.GetBytes(keysize);
            var (plain, count) = Decrypt(payload, iv, password);
            var data = new byte[count];
            Buffer.BlockCopy(plain, 0, data, 0, count);
            return deserialize(data);
        }

        private async Task<byte[]> EncryptAsync(byte[] data, byte[] salt, byte[] iv, byte[] key, CancellationToken token = default)
        {
            algorithm.Mode = CipherMode;
            algorithm.Padding = PaddingMode;
            using var enc = algorithm.CreateEncryptor(key, iv);
            using var mstream = new MemoryStream();
            using var cstream = new CryptoStream(mstream, enc, CryptoStreamMode.Write);
            await cstream.WriteAsync(data.AsMemory(0, data.Length), token);
            await cstream.FlushFinalBlockAsync(token);

            //add salt to iteration vector and data in memory stream
            var cipher = salt.Concat(iv).Concat(mstream.ToArray()).ToArray();
            return cipher;
        }

        public Task<byte[]> EncryptAsync<T>(T data, Func<T, byte[]> serialize, byte[] key, CancellationToken token = default)
            => EncryptAsync(serialize(data), key, token);

        public Task<byte[]> EncryptAsync(byte[] data, byte[] key, CancellationToken token = default)
        {
            var blocks = algorithm.BlockSize / 8;
            var keysize = algorithm.KeySize / 8;
            var salt = new byte[blocks];
            var iv = new byte[blocks];

            CryptoProviderExtensions.FillWithRandomBytes(salt);
            CryptoProviderExtensions.FillWithRandomBytes(iv);

            using var derived = new Rfc2898DeriveBytes(key, salt, iterations);
            var password = derived.GetBytes(keysize);
            return EncryptAsync(data, salt, iv, password, token);
        }

        private async Task<(byte[] plain, int count)> DecryptAsync(byte[] cipher, byte[] iv, byte[] key, CancellationToken token = default)
        {
            algorithm.Mode = CipherMode;
            algorithm.Padding = PaddingMode;
            using var transform = algorithm.CreateDecryptor(key, iv);
            using var mstream = new MemoryStream(cipher);
            using var cstream = new CryptoStream(mstream, transform, CryptoStreamMode.Read);
            var buffer = new byte[cipher.Length];
            var pending = cipher.Length;
            var received = 0;
            do
            {
                var read = await cstream.ReadAsync(buffer, received, pending, token).ConfigureAwait(false);
                if (read == 0) break;
                received += read;
                pending -= read;
            } while (pending > 0);

            return (buffer, received);
        }

        public async Task<T> DecryptAsync<T>(byte[] cipher, Func<byte[], T> deserialize, byte[] key, CancellationToken token = default)
        {
            var blocks = algorithm.BlockSize / 8;
            var keysize = algorithm.KeySize / 8;
            var salt = cipher.Take(blocks).ToArray();
            var iv = cipher.Skip(blocks).Take(blocks).ToArray();
            var payload = cipher.Skip(2 * blocks).Take(cipher.Length - (2 * blocks)).ToArray();

            using var derived = new Rfc2898DeriveBytes(key, salt, iterations);
            var password = derived.GetBytes(keysize);
            var (plain, count) = await DecryptAsync(payload, iv, password, token).ConfigureAwait(false);
            var data = new byte[count];
            Buffer.BlockCopy(plain, 0, data, 0, count);
            return deserialize(data);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    if (algorithm != null)
                    {
                        algorithm.Clear();
                        algorithm.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
