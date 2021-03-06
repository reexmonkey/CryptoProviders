using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class HMACSHA1Signer : HMACSigner
    {
        public HMACSHA1Signer() : base(key => new HMACSHA1(key))
        {
        }
    }
}