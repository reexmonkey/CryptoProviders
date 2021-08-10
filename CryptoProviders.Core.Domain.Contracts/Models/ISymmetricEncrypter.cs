using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface ISymmetricEncrypter
    {
        byte[] Encrypt<T>(T data, Func<T, byte[]> serialize, byte[] key);

        byte[] Encrypt(byte[] data, byte[] key);

        T Decrypt<T>(byte[] cipher, Func<byte[], T> deserialize, byte[] key);

        Task<byte[]> EncryptAsync<T>(T data, Func<T, byte[]> serialize, byte[] key, CancellationToken token = default);

        Task<byte[]> EncryptAsync(byte[] data, byte[] key, CancellationToken token = default);

        Task<T> DecryptAsync<T>(byte[] cipher, Func<byte[], T> deserialize, byte[] key, CancellationToken token = default);
    }
}