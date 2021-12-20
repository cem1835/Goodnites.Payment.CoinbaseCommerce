using Volo.Abp.Reflection;

namespace Goodnites.Payment.CoinbaseCommerce.Permissions
{
    public class CoinbaseCommercePermissions
    {
        public const string GroupName = "CoinbaseCommerce";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CoinbaseCommercePermissions));
        }
    }
}