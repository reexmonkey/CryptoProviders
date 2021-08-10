using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class AesEncrypter : SymmetricEncrypter
    {
        public AesEncrypter(int iterations = DefaultIterations, PaddingMode paddingMode = PaddingMode.PKCS7)
            : base(Aes.Create(), iterations, CipherMode.CBC, paddingMode)
        {
        }
    }
}