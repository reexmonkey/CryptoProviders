namespace Reexmonkey.CryptoProviders.Core.Domain.Contracts.Models
{
    public interface IAsymmetricKeyPair
    {
        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        void ImportPrivateKey(string key, string password = null);

        void ImportPublicKey(string key);

        string ExportPrivateKey(string password = null);

        string ExportPublicKey();
    }
}
