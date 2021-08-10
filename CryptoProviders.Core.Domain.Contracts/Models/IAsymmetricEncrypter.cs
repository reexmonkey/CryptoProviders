using System;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface IAsymmetricEncrypter
    {
        byte[] Encrypt(byte[] data, string publicKey);

        byte[] Decrypt(byte[] cipher, string privateKey, string password = null);

        (byte[] cipher, byte[] signature) EncryptKey(byte[] key, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null);

        byte[] DecryptKey<T>(byte[] cipher, byte[] signature, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null);
    }
}
