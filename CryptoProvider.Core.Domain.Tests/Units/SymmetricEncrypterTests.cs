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
    [Collection(nameof(SymmetricEncrypterFixtureCollection))]
    public sealed class SymmetricEncrypterTests
    {
        private readonly SymmetricEncrypterFixture fixture;
        private readonly ITestOutputHelper console;

        public SymmetricEncrypterTests(SymmetricEncrypterFixture fixture, ITestOutputHelper console)
        {
            this.fixture = fixture;
            this.console = console;
        }

        public static IEnumerable<object[]> GetEncrypters()
        {
            yield return new object[] { new RijndaelEncrypter() };
            yield return new object[] { new AesEncrypter() };
            yield return new object[] { new DesEncrypter() };
            yield return new object[] { new Rc2Encrypter() };
            yield return new object[] { new TripleDesEncrypter() };
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public void ShouldEncryptPlaintext(ISymmetricEncrypter encrypter)
        {
            //arrange
            const string plaintext = "The quick brown fox jumps over the lazy dogs";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            var secret = serialize(fixture.Secret);

            //act
            var cipher = encrypter.Encrypt(plaintext, serialize, secret);

            //assert
            Assert.NotNull(cipher);
            console.WriteLine($"plaintext: {plaintext}");
            console.WriteLine($"ciphertext: {fixture.ToBase64String(cipher)}");
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public void ShouldDecryptPlaintext(ISymmetricEncrypter encrypter)
        {
            //arrange
            const string plaintext = "Portez ce vieux whisky au juge blond qui fume";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            string deserialize(byte[] value) => fixture.Encoding.GetString(value);

            var secret = serialize(fixture.Secret);
            var cipher = encrypter.Encrypt(plaintext, serialize, secret);

            //act
            var result = encrypter.Decrypt(cipher, deserialize, secret);

            //assert
            Assert.NotNull(cipher);
            Assert.Equal(plaintext, result);
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public void ShouldEncryptDataObject(ISymmetricEncrypter encrypter)
        {
            //arrange
            var plain = new FooBar
            {
                Name = "Foo",
            };

            byte[] serialize<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            var secret = fixture.GenerateRandomKey(16);

            //act
            var cipher = encrypter.Encrypt(plain, serialize, secret);

            //assert
            Assert.NotNull(cipher);
            console.WriteLine($"ciphertext: {fixture.ToBase64String(cipher)}");
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public void ShouldDecryptDataObject(ISymmetricEncrypter encrypter)
        {
            //arrange
            var plain = new FooBar
            {
                Name = "Bar",
            };

            byte[] serialize<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            T deserialize<T>(byte[] value) => fixture.BinarySerializer.Deserialize<T>(value);

            var secret = fixture.GenerateRandomKey(16);
            var cipher = encrypter.Encrypt(plain, serialize, secret);

            //act
            var result = encrypter.Decrypt(cipher, deserialize<FooBar>, secret);

            //assert
            Assert.NotNull(cipher);
            Assert.Equal(plain, result);
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public async Task ShouldEncryptPlaintextAsync(ISymmetricEncrypter encrypter)
        {
            //arrange
            const string plaintext = "The quick brown fox jumps over the lazy dogs";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            var secret = serialize(fixture.Secret);

            //act
            var cipher = await encrypter.EncryptAsync(plaintext, serialize, secret);

            //assert
            Assert.NotNull(cipher);
            console.WriteLine($"plaintext: {plaintext}");
            console.WriteLine($"ciphertext: {fixture.ToBase64String(cipher)}");
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public async Task ShouldDecryptPlaintextAsync(ISymmetricEncrypter encrypter)
        {
            //arrange
            const string plaintext = "Portez ce vieux whisky au juge blond qui fume";
            byte[] serialize(string value) => fixture.Encoding.GetBytes(value);
            string deserialize(byte[] value) => fixture.Encoding.GetString(value);

            var secret = serialize(fixture.Secret);
            var cipher = await encrypter.EncryptAsync(plaintext, serialize, secret);

            //act
            var result = await encrypter.DecryptAsync(cipher, deserialize, secret);

            //assert
            Assert.NotNull(cipher);
            Assert.Equal(plaintext, result);
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public async Task ShouldEncryptDataObjectAsync(ISymmetricEncrypter encrypter)
        {
            //arrange
            var plain = new FooBar
            {
                Name = "Foo",
            };

            byte[] serialize<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            var secret = fixture.GenerateRandomKey(16);

            //act
            var cipher = await encrypter.EncryptAsync(plain, serialize, secret);

            //assert
            Assert.NotNull(cipher);
            console.WriteLine($"ciphertext: {fixture.ToBase64String(cipher)}");
        }

        [Theory]
        [MemberData(nameof(GetEncrypters))]
        public async Task ShouldDecryptDataObjectAsync(ISymmetricEncrypter encrypter)
        {
            //arrange
            var plain = new FooBar
            {
                Name = "Bar",
            };

            byte[] serialize<T>(T value) => fixture.BinarySerializer.Serialize<T>(value);
            T deserialize<T>(byte[] value) => fixture.BinarySerializer.Deserialize<T>(value);

            var secret = fixture.GenerateRandomKey(16);
            var cipher = await encrypter.EncryptAsync(plain, serialize, secret);

            //act
            var result = await encrypter.DecryptAsync(cipher, deserialize<FooBar>, secret);

            //assert
            Assert.NotNull(cipher);
            Assert.Equal(plain, result);
        }
    }
}
