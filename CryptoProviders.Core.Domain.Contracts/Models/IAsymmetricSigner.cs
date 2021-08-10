using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface IAsymmetricSigner
    {
        byte[] Sign(byte[] data, string privateKey, string password = null);

        bool Verify(byte[] data, byte[] signature, string publicKey);

        Task<byte[]> SignAsync(byte[] data, string privateKey, string password = null, CancellationToken token = default);

        Task<bool> VerifyAsync(byte[] data, byte[] signature, string publicKey, CancellationToken token = default);
    }
}
