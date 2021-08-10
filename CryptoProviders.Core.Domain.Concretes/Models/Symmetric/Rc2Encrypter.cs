using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions;
using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
public sealed class Rc2Encrypter : SymmetricEncrypter
    {
        public Rc2Encrypter(int iterations = DefaultIterations, PaddingMode paddingMode = PaddingMode.PKCS7)
            : base(RC2.Create(), iterations, CipherMode.CBC, paddingMode)
        {
        }
    }
}