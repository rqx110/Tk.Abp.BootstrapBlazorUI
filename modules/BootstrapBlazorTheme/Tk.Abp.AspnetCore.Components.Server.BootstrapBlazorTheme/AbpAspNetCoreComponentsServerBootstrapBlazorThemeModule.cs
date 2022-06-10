using Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme.Bundling;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme;
using Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Server;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages;
using Volo.Abp.Modularity;

namespace Tk.Abp.AspnetCore.Components.Server.BootstrapBlazorTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerModule),
    typeof(AbpAspNetCoreMvcUiPackagesModule),
    typeof(AbpAspNetCoreMvcUiBundlingModule),
    typeof(AbpAspNetCoreComponentsWebBootstrapBlazorThemeModule)
)]
public class AbpAspNetCoreComponentsServerBootstrapBlazorThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorStandardBundles.Styles.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorStandardBundles.Scripts.Global, bundle =>
                {
                    bundle.AddContributors(typeof(BlazorGlobalScriptContributor));
                });
            
            options
                .StyleBundles
                .Add(BlazorBootstrapBlazorThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorBootstrapBlazorThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorBootstrapBlazorThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorBootstrapBlazorThemeScriptContributor));
                });
        });
        
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new BootstrapBlazorThemeToolbarContributor());
        });
    }
}
