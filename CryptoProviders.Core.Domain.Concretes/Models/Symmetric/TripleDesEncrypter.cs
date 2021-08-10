using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions;
using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.IO;
using System.Linq;
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