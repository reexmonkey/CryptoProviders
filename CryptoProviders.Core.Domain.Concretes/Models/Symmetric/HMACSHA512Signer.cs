using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
public sealed class HMACSHA512Signer : HMACSigner
    {
        public HMACSHA512Signer() : base(key => new HMACSHA512(key))
        {
        }
    }
}