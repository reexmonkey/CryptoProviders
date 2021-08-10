using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public abstract class HMACSigner : ISymmetricSigner
    {
        protected readonly Func<byte[], HMAC> transform;

        public HMACSigner(Func<byte[], HMAC> transform)
        {
            this.transform = transform ?? throw new ArgumentNullException(nameof(transform));
        }

        public byte[] Sign<T>(T data, Func<T, byte[]> serialize, byte[] key)
            => Sign(serialize(data), key);

        public byte[] Sign(byte[] data, byte[] key)
        {
            using var hmac = transform(key);
            return hmac.ComputeHash(data);
        }

        public bool Verify<T>(T data, byte[] signature, byte[] key, Func<T, byte[]> serialize)
            => Verify(serialize(data), signature, key);

        public bool Verify(byte[] data, byte[] signature, byte[] key)
        {
            using var hmac = transform(key);
            var hash = hmac.ComputeHash(data);
            return hash.SequenceEqual(signature);
        }

        public Task<byte[]> SignAsync<T>(T data, Func<T, byte[]> serialize, byte[] key, CancellationToken token = default)
            => SignAsync(serialize(data), key, token);

        public async Task<byte[]> SignAsync(byte[] data, byte[] key, CancellationToken token = default)
        {
            using var hmac = transform(key);
            using var stream = new MemoryStream(data);
            return await hmac.ComputeHashAsync(stream, token).ConfigureAwait(false);
        }

        public Task<bool> VerifyAsync<T>(T data, byte[] signature, byte[] key, Func<T, byte[]> serialize, CancellationToken token = default)
            => VerifyAsync(serialize(data), signature, key, token);

        public async Task<bool> VerifyAsync(byte[] data, byte[] signature, byte[] key, CancellationToken token = default)
        {
            using var hmac = transform(key);
            using var stream = new MemoryStream(data);
            var hash = await hmac.ComputeHashAsync(stream, token).ConfigureAwait(false);
            return hash.SequenceEqual(signature);
        }
    }
}
