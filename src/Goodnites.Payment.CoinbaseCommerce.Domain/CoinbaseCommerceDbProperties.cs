namespace Goodnites.Payment.CoinbaseCommerce
{
    public static class CoinbaseCommerceDbProperties
    {
        public static string DbTablePrefix { get; set; } = "CoinbaseCommerce";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "CoinbaseCommerce";
    }
}
