using AutoMapper;
using Volo.Abp.TenantManagement;

namespace Tk.Abp.TenantManagement.Blazor.BootstrapBlazorUI;

public class AbpTenantManagementBlazorAutoMapperProfile : Profile
{
    public AbpTenantManagementBlazorAutoMapperProfile()
    {
        CreateMap<TenantDto, TenantUpdateDto>()
            .MapExtraProperties();
    }
}
