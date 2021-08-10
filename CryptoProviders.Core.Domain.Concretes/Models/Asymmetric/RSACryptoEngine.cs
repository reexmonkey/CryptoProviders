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
    }
}
