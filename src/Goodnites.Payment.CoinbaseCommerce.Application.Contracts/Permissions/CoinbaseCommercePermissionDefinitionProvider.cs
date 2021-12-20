using Goodnites.Payment.CoinbaseCommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Goodnites.Payment.CoinbaseCommerce.Permissions
{
    public class CoinbaseCommercePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CoinbaseCommercePermissions.GroupName, L("Permission:CoinbaseCommerce"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CoinbaseCommerceResource>(name);
        }
    }
}