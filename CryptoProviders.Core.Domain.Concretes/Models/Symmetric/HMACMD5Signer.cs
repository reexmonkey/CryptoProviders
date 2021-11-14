using System.Security.Cryptography;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric
{
    public sealed class HMACMD5Signer : HMACSigner
    {
        public HMACMD5Signer() : base(key => new HMACMD5(key))
        {
        }
    }
}