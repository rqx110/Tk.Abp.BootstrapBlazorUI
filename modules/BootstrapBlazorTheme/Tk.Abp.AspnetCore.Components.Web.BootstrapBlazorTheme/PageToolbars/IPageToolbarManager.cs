using System.Threading.Tasks;

namespace Tk.Abp.AspnetCore.Components.Web.BootstrapBlazorTheme.PageToolbars;

public interface IPageToolbarManager
{
    Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
}
