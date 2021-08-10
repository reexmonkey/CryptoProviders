using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public abstract class AsymmetricCryptoEngine : IAsymmetricEncrypter, IAsymmetricSigner
    {
        public (byte[] cipher, byte[] signature) EncryptKey(byte[] key, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null)
        {
            //1. Sign data
            var hash = Sign(key, privateKey, password);

            //2. Combine data and hash
            var buffer = new byte[hash.Length + key.Length];
            Buffer.BlockCopy(hash, 0, buffer, 0, hash.Length); //copy hash to buffer
            Buffer.BlockCopy(key, 0, buffer, hash.Length, key.Length); //copy data to buffer

            //3. Encrypt plain and hash
            var cipher = Encrypt(buffer, publicKey);

            //4. Sign again with hash of public key to prevent naive 'Encryption & Sign' vulnerabilities
            var signature = Sign(serialize(publicKey), privateKey, password);

            return (cipher, signature);
        }

        public byte[] DecryptKey<T>(byte[] cipher, byte[] signature, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public abstract byte[] Encrypt(byte[] data, string publicKey);

        public abstract byte[] Decrypt(byte[] cipher, string privateKey, string password = null);

        public abstract byte[] Sign<T>(T data, Func<T, byte[]> serialize, string privateKey, string password = null);

        public abstract byte[] Sign(byte[] data, string privateKey, string password = null);

        public abstract bool Verify<T>(T data, Func<T, byte[]> serialize, byte[] signature, string privateKey, string password = null);

        public abstract bool Verify(byte[] data, byte[] signature, string privateKey, string password = null);
    }
}
