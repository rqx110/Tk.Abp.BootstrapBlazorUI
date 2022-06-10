using Tk.Abp.BootstrapBlazorUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme;

[DependsOn(
    typeof(AbpBootstrapBlazorUIModule),
    typeof(AbpUiNavigationModule)
)]
public class AbpAspNetCoreComponentsWebBootstrapBlazorThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}
