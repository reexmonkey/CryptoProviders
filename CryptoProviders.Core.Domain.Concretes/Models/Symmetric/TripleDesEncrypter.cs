using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class TripleDesEncrypter : SymmetricEncrypter
    {
        public TripleDesEncrypter(int iterations = DefaultIterations, PaddingMode paddingMode = PaddingMode.PKCS7)
            : base(TripleDES.Create(), iterations, CipherMode.CBC, paddingMode)
        {
        }
    }
}