using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions
{
    public static class CryptoProviderExtensions
    {
        public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, byte[] buffer, int offset, int count, CancellationToken token = default)
        {
            if (buffer is null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0 || count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));

            using var stream = new MemoryStream();
            stream.Seek(0, SeekOrigin.End);
            await stream.WriteAsync(buffer, offset, count, token);
            return await algorithm.ComputeHashAsync(stream, token);
        }

        public static Task<byte[]> ComputeHashAsync(this HashAlgorithm algorithm, byte[] buffer, CancellationToken token = default)
        {
            if (buffer is null) throw new ArgumentNullException(nameof(buffer));
            return algorithm.ComputeHashAsync(buffer, 0, buffer.Length, token);
        }

        public static void FillWithRandomBytes(byte[] buffer)
        {
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(buffer);
        }
    }
}
