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