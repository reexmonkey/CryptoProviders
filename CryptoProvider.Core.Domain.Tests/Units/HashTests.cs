using Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures;
using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Units
{
    [Collection(nameof(HashFixtureCollection))]
    public sealed class HashTests
    {
        private readonly HashFixture fixture;
        private readonly ITestOutputHelper console;

        public HashTests(HashFixture fixture, ITestOutputHelper console)
        {
            this.fixture = fixture;
            this.console = console;
        }

        public static IEnumerable<object[]> GetHMACs()
        {
            yield return new object[] { new HMACMD5() };
            yield return new object[] { new HMACSHA1() };
            yield return new object[] { new HMACSHA256() };
            yield return new object[] { new HMACSHA384() };
            yield return new object[] { new HMACSHA512() };
        }

        [Theory]
        [MemberData(nameof(GetHMACs))]
        public async Task ShouldComputeHashAsync(HMAC hmac)
        {
            //arrange
            var key = fixture.Encoding.GetBytes("123456");
            var plaintext = fixture.Encoding.GetBytes("Lorem ipsum dolor sit amet, consectetur adipiscing elit");
            hmac.Key = key;

            //act
            var hash = await hmac.ComputeHashAsync(plaintext);

            //assert
            Assert.NotNull(hash);
            console.WriteLine(fixture.ToHexString(hash));
        }
    }
}
