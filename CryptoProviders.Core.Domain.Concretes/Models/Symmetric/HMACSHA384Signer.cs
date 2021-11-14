using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class HMACSHA384Signer : HMACSigner
    {
        public HMACSHA384Signer() : base(key => new HMACSHA384(key))
        {
        }
    }
}