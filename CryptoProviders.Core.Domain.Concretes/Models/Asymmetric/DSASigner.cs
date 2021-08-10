using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public sealed class DSASigner : IAsymmetricSigner
    {
        public byte[] Sign(byte[] data, string privateKey, string password = null)
        {
            throw new NotImplementedException();
        }

        public bool Verify(byte[] data, byte[] signature, string publicKey)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> SignAsync(byte[] data, string privateKey, string password = null, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyAsync(byte[] data, byte[] signature, string publicKey, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
