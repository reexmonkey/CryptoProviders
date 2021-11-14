using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class DesEncrypter : SymmetricEncrypter
    {
        public DesEncrypter(int iterations = DefaultIterations, PaddingMode paddingMode = PaddingMode.PKCS7)
            : base(DES.Create(), iterations, CipherMode.CBC, paddingMode)
        {
        }
    }
}