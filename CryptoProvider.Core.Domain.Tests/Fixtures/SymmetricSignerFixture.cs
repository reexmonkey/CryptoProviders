using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models;
using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures
{
    public class SymmetricSignerFixture : FixtureBase
    {
        public string Secret { get; } = "d0Qvcz1sJGVPR25JITY9Rz81";
    }
}
