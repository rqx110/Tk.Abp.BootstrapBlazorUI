using System.Threading.Tasks;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.PageToolbars;

public abstract class PageToolbarContributor : IPageToolbarContributor
{
    public abstract Task ContributeAsync(PageToolbarContributionContext context);
}
