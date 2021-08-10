using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions
{
    public static class CryptoProviderExtensions
    {
        /// <summary>
        /// Credits: <seealso cref="https://gist.github.com/sebnilsson/e12a96cd07e5b044eea7bc8b7477b20d"/>
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="stream"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, Stream stream, CancellationToken token = default)
        {
            if (algorithm is null) throw new ArgumentNullException(nameof(algorithm));
            if (stream is null) throw new ArgumentNullException(nameof(stream));

            const int bufferSize = 4096; //default 4K
            var buffer = new byte[bufferSize];
            var len = (int)stream.Length;

            algorithm.Initialize();

            do
            {
                var read = await stream.ReadAsync(buffer, 0, bufferSize, token).ConfigureAwait(false);
                algorithm.TransformBlock(buffer, 0, read, default, default);

                if (stream.Position == len)
                {
                    algorithm.TransformFinalBlock(buffer, 0, read);
                    break;
                }
            }
            while (stream.Position <= len);

            return algorithm.Hash;
        }

        public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, byte[] buffer, int offset, int count, CancellationToken token = default)
        {
            if (buffer is null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0 || count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));

            using (var stream = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.End);
                await stream.WriteAsync(buffer, offset, count, token);
                return await algorithm.ComputeHashAsync(stream, token);
            }
        }

        public static Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, byte[] buffer, CancellationToken token = default)
        {
            if (buffer is null) throw new ArgumentNullException(nameof(buffer));
            return algorithm.ComputeHashAsync(buffer, 0, buffer.Length, token);
        }

        public static void FillWithRandomBytes(byte[] buffer)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(buffer);
            }
        }
    }
}
