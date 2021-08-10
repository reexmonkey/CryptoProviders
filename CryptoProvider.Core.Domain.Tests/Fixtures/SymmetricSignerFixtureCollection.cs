using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(SymmetricSignerFixtureCollection))]
    public class SymmetricSignerFixtureCollection : ICollectionFixture<SymmetricSignerFixture>
    {
    }
}
