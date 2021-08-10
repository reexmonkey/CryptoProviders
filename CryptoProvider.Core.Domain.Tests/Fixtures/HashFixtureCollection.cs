using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(HashFixtureCollection))]
    public class HashFixtureCollection : ICollectionFixture<HashFixture>
    {
    }
}
