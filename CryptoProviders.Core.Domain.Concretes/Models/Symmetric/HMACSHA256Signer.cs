using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
public sealed class HMACSHA256Signer : HMACSigner
    {
        public HMACSHA256Signer() : base(key => new HMACSHA256(key))
        {
        }
    }
}