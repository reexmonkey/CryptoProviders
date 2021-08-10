using System;

namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface IAsymmetricSigner
    {
        byte[] Sign<T>(T data, Func<T, byte[]> serialize, string privateKey, string password = null);

        byte[] Sign(byte[] data, string privateKey, string password = null);

        bool Verify<T>(T data, Func<T, byte[]> serialize, byte[] signature, string privateKey, string password = null);

        bool Verify(byte[] data, byte[] signature, string privateKey, string password = null);
    }
}
