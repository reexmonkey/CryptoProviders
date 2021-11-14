using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public abstract class CryptoEngine : IAsymmetricEncrypter, IAsymmetricSigner
    {
        public (byte[] cipher, byte[] isignature, byte[] osignature) Encrypt(byte[] data, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null)
        {
            //1. Sign data
            var isignature = Sign(data, privateKey, password);

            //2. Combine data and inner siganture
            var buffer = new byte[isignature.Length + data.Length];
            Buffer.BlockCopy(isignature, 0, buffer, 0, isignature.Length); //copy hash to buffer
            Buffer.BlockCopy(data, 0, buffer, isignature.Length, data.Length); //copy data to buffer

            //3. Encrypt combination of data and inner signature
            var cipher = Encrypt(buffer, publicKey);

            //4. Sign again with digital hash of public key to prevent naive 'Encryption & Sign' vulnerability
            var osignature = Sign(serialize(publicKey), privateKey, password);

            //5. Return tuple of cipher, data signature and public key signature
            return (cipher, isignature, osignature);
        }

        public byte[] Decrypt(byte[] cipher, byte[] osignature, byte[] isignature, string publicKey, Func<string, byte[]> serialize, string privateKey, string password = null)
        {
            //1. Verify public key signature
            var valid = Verify(serialize(publicKey), osignature, publicKey);
            if (!valid) throw new InvalidOperationException("´Verification of public key signature failed.");

            //2. Decrypt cipher
            var buffer = Decrypt(cipher, privateKey, password);

            //3. Extract payload data
            var data = buffer.Skip(isignature.Length).Take(buffer.Length - isignature.Length).ToArray();

            //4. Verify data signature
            valid = Verify(data, isignature, publicKey);

            //5. Return data if inner signature passed test
            return valid ? data : throw new InvalidOperationException("´Verification of data signature failed.");
        }

        public abstract byte[] Encrypt(byte[] data, string publicKey);

        public abstract byte[] Decrypt(byte[] cipher, string privateKey, string password = null);

        public abstract byte[] Sign(byte[] data, string privateKey, string password = null);

        public abstract bool Verify(byte[] data, byte[] signature, string publicKey);

        public abstract Task<byte[]> SignAsync(byte[] data, string privateKey, string password = null, CancellationToken token = default);

        public abstract Task<bool> VerifyAsync(byte[] data, byte[] signature, string publicKey, CancellationToken token = default);
    }
}
