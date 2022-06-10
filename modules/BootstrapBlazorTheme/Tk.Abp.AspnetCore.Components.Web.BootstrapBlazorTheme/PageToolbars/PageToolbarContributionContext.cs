using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.PageToolbars;

public class PageToolbarContributionContext
{
    [NotNull]
    public IServiceProvider ServiceProvider { get; }

    [NotNull]
    public PageToolbarItemList Items { get; }

    public PageToolbarContributionContext(
        [NotNull] IServiceProvider serviceProvider)
    {
        ServiceProvider = Check.NotNull(serviceProvider, nameof(serviceProvider));
        Items = new PageToolbarItemList();
    }
}
