using Volo.Abp.Settings;

namespace Goodnites.Payment.CoinbaseCommerce.Settings
{
    public class CoinbaseCommerceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(CoinbaseCommerceSettings.CoinBaseApiKey,isEncrypted:true));
            context.Add(new SettingDefinition(CoinbaseCommerceSettings.CoinBaseWebHookKey,isEncrypted:true));
            context.Add(new SettingDefinition(CoinbaseCommerceSettings.CoinBaseMin,defaultValue:"1"));
            context.Add(new SettingDefinition(CoinbaseCommerceSettings.CoinBaseMax,defaultValue:"10000"));
        }
    }
}