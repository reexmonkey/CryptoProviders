using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(SymmetricEncrypterFixtureCollection))]
    public class SymmetricEncrypterFixtureCollection : ICollectionFixture<SymmetricEncrypterFixture>
    {
    }
}
