namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.PageToolbars;

public class PageToolbar
{
    public PageToolbarContributorList Contributors { get; set; }

    public PageToolbar()
    {
        Contributors = new PageToolbarContributorList();
    }
}
