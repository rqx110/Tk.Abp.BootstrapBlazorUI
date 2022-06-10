using System.Reflection;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Routing;

public class AbpRouterOptions
{
    public Assembly AppAssembly { get; set; }

    public RouterAssemblyList AdditionalAssemblies { get; }

    public AbpRouterOptions()
    {
        AdditionalAssemblies = new RouterAssemblyList();
    }
}
