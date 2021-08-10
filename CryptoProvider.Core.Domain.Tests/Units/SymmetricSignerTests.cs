using Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures;
using Reexmonkey.CryptoProvider.Core.Domain.Tests.Helpers;
using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Models.Symmetric;
using Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Units
{
    [Collection(nameof(SymmetricSignerFixtureCollection))]
    public sealed class SymmetricSignerTests
    {
        private readonly SymmetricSignerFixture fixture;
        private readonly ITestOutputHelper console;

        public SymmetricSignerTests(SymmetricSignerFixture fixture, ITestOutputHelper console)
        {
            this.fixture = fixture;
            this.console = console;
        }

        public static IEnumerable<object[]> GetSigners()
        {
            yield return new object[] { new HMACMD5Signer() };
            yield return new object[] { new HMACSHA1Signer() };
            yield return new object[] { new HMACSHA256Signer() };
            yield return new object[] { new HMACSHA384Signer() };
            yield return new object[] { new HMACSHA512Signer() };
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public void ShouldSignTextValue(ISymmetricSigner signer)
        {
            //arrange
            const string value = "Victor jagt zwölf Boxkämpfer quer über den großen Sylter";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);

            //act
            var signature = signer.Sign(serialize(value), serialize(fixture.Secret));

            //assert
            Assert.NotNull(signature);
            console.WriteLine($"value: {value}");
            console.WriteLine($"signature: {fixture.ToBase64String(signature)}");
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public void ShouldVerifyTextValueSignature(ISymmetricSigner signer)
        {
            //arrange
            const string value = "Portez ce vieux whisky au juge blond qui fume";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            var data = serialize(value);
            var key = serialize(fixture.Secret);

            var signature = signer.Sign(data, key);

            //act
            var success = signer.Verify(data, signature, key);

            //assert
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public void ShouldSignDataObject(ISymmetricSigner signer)
        {
            //arrange
            var value = new FooBar
            {
                Name = "Bar",
            };

            byte[] serializeData<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            byte[] serializeText(string value) => fixture.Encoding.GetBytes(value);

            //act
            var signature = signer.Sign(serializeData(value), serializeText(fixture.Secret));

            //assert
            Assert.NotNull(signature);
            console.WriteLine($"signature: {fixture.ToBase64String(signature)}");
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public void ShouldVerifyDataObjectSignature(ISymmetricSigner signer)
        {
            //arrange
            var value = new FooBar
            {
                Name = "Bar",
            };

            byte[] serializeData<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            byte[] serializeText(string value) => fixture.Encoding.GetBytes(value);
            var data = serializeData(value);
            var key = serializeText(fixture.Secret);

            var signature = signer.Sign(data, key);

            //act
            var success = signer.Verify(data, signature, key);

            //assert
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public async Task ShouldSignTextValueAsync(ISymmetricSigner signer)
        {
            //arrange
            const string value = "Victor jagt zwölf Boxkämpfer quer über den großen Sylter";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);

            //act
            var signature = await signer.SignAsync(serialize(value), serialize(fixture.Secret));

            //assert
            Assert.NotNull(signature);
            console.WriteLine($"value: {value}");
            console.WriteLine($"signature: {fixture.ToBase64String(signature)}");
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public async Task ShouldVerifyTextValueSignatureAsync(ISymmetricSigner signer)
        {
            //arrange
            const string value = "Portez ce vieux whisky au juge blond qui fume";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            var data = serialize(value);
            var key = serialize(fixture.Secret);

            var signature = await signer.SignAsync(value, serialize, key);

            //act
            var success = await signer.VerifyAsync(data, signature, key);

            //assert
            Assert.True(success);
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public async Task ShouldSignDataObjectAsync(ISymmetricSigner signer)
        {
            //arrange
            var value = new FooBar
            {
                Name = "Bar",
            };

            byte[] serializeData<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            byte[] serializeText(string value) => fixture.Encoding.GetBytes(value);

            //act
            var signature = await signer.SignAsync(serializeData(value), serializeText(fixture.Secret));

            //assert
            Assert.NotNull(signature);
            console.WriteLine($"signature: {fixture.ToBase64String(signature)}");
        }

        [Theory]
        [MemberData(nameof(GetSigners))]
        public async Task ShouldVerifyDataObjectAsync(ISymmetricSigner signer)
        {
            //arrange
            var value = new FooBar
            {
                Name = "Bar",
            };

            byte[] serializeData<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            byte[] serializeText(string value) => fixture.Encoding.GetBytes(value);
            var data = serializeData(value);
            var key = serializeText(fixture.Secret);

            var signature = await signer.SignAsync(data, key);

            //act
            var success = await signer.VerifyAsync(data, signature, key);

            //assert
            Assert.True(success);
        }
    }
}
