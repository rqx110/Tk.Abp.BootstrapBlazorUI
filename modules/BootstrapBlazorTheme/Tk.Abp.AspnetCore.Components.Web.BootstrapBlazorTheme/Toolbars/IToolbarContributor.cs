using System.Threading.Tasks;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
