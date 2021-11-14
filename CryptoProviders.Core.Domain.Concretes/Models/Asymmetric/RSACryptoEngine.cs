using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public sealed class RSACryptoEngine : CryptoEngine
    {
        public RSAEncryptionPadding EncryptionPadding { get; }

        public RSASignaturePadding SignaturePadding { get; }

        public override byte[] Decrypt(byte[] cipher, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encrypt(byte[] data, string publicKey)
        {
            throw new NotImplementedException();
        }

        public override byte[] Sign(byte[] data, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> SignAsync(byte[] data, string privateKey, string password = null, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public override bool Verify(byte[] data, byte[] signature, string publicKey)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> VerifyAsync(byte[] data, byte[] signature, string publicKey, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
