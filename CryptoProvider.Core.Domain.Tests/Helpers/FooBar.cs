using System;
using System.Collections.Generic;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Helpers
{
    public sealed class FooBar : IEquatable<FooBar>
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public DateTime CreatedDateUtc { get; private set; }

        public FooBar()
        {
            Id = Guid.NewGuid();
            CreatedDateUtc = DateTime.UtcNow;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FooBar);
        }

        public bool Equals(FooBar other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(FooBar left, FooBar right)
        {
            return EqualityComparer<FooBar>.Default.Equals(left, right);
        }

        public static bool operator !=(FooBar left, FooBar right)
        {
            return !(left == right);
        }
    }
}