using System;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface IAsymmetricEncrypter
    {
        byte[] Encrypt(byte[] data, string publicKey);

        byte[] Decrypt(byte[] cipher, string privateKey, string password = null);

        (byte[] cipher, byte[] isignature, byte[] osignature) Encrypt(byte[] data, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null);

        byte[] Decrypt(byte[] cipher, byte[] osignature, byte[] isignature, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null);
    }
}
