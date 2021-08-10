using System;
using System.Collections.Generic;
using System.Text;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public sealed class DSACryptoEngine : AsymmetricCryptoEngine
    {
        public override byte[] Decrypt(byte[] cipher, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override byte[] Encrypt(byte[] data, string publicKey)
        {
            throw new NotImplementedException();
        }

        public override byte[] Sign<T>(T data, Func<T, byte[]> serialize, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override byte[] Sign(byte[] data, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override bool Verify<T>(T data, Func<T, byte[]> serialize, byte[] signature, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public override bool Verify(byte[] data, byte[] signature, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
