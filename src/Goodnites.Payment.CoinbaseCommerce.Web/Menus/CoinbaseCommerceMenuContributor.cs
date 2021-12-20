using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Goodnites.Payment.CoinbaseCommerce.Web.Menus
{
    public class CoinbaseCommerceMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(CoinbaseCommerceMenus.Prefix, displayName: "CoinbaseCommerce", "~/CoinbaseCommerce", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}