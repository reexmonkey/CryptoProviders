using System;
using System.Collections.Generic;

namespace Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Asymmetric
{
    public abstract class AsymmetricKeyPair : IEquatable<AsymmetricKeyPair>
    {
        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as AsymmetricKeyPair);
        }

        public bool Equals(AsymmetricKeyPair other)
        {
            return other != null &&
                   PublicKey.Equals(other.PublicKey, StringComparison.Ordinal) &&
                   PrivateKey.Equals(other.PrivateKey, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PublicKey, PrivateKey);
        }

        public static bool operator ==(AsymmetricKeyPair left, AsymmetricKeyPair right)
        {
            return EqualityComparer<AsymmetricKeyPair>.Default.Equals(left, right);
        }

        public static bool operator !=(AsymmetricKeyPair left, AsymmetricKeyPair right) => !(left == right);
    }
}
