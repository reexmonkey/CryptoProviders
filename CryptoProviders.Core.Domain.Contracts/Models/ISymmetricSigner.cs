using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface ISymmetricSigner
    {
        byte[] Sign<T>(T data, Func<T, byte[]> serialize, byte[] key);

        byte[] Sign(byte[] data, byte[] key);

        bool Verify<T>(T data, byte[] signature, byte[] key, Func<T, byte[]> serialize);

        bool Verify(byte[] data, byte[] signature, byte[] key);

        Task<byte[]> SignAsync<T>(T data, Func<T, byte[]> serialize, byte[] key, CancellationToken token = default);

        Task<byte[]> SignAsync(byte[] data, byte[] key, CancellationToken token = default);

        Task<bool> VerifyAsync<T>(T data, byte[] signature, byte[] key, Func<T, byte[]> serialize, CancellationToken token = default);

        Task<bool> VerifyAsync(byte[] data, byte[] signature, byte[] key, CancellationToken token = default);
    }
}