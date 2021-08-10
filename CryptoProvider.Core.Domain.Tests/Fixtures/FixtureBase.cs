using reexmonkey.xmisc.backbone.io.messagepack.serializers;
using reexmonkey.xmisc.core.io.serializers;
using Reexmonkey.CryptoProviders.Core.Domain.Concretes.Extensions;
using System;
using System.Text;

namespace Reexmonkey.CryptoProvider.Core.Domain.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        public Encoding Encoding = new UTF8Encoding(false, true);

        public BinarySerializerBase BinarySerializer { get; }

        protected FixtureBase()
        {
            BinarySerializer = new MessagePackSerializer();
        }

        public byte[] GenerateRandomKey(int keysize)
        {
            var key = new byte[keysize];
            CryptoProviderExtensions.FillWithRandomBytes(key);
            return key;
        }

        public string ToHexString(byte[] data)
          => BitConverter.ToString(data).Replace("-", string.Empty);

        public string ToBase64String(byte[] data)
          => Convert.ToBase64String(data, Base64FormattingOptions.InsertLineBreaks);
    }
}
