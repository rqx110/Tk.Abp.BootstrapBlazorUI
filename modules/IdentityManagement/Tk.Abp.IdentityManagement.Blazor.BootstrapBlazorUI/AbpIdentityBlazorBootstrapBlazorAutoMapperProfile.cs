using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Tk.Abp.IdentityManagement.Blazor.BootstrapBlazorUI;

public class AbpIdentityBlazorBootstrapBlazorAutoMapperProfile: Profile
{
    public AbpIdentityBlazorBootstrapBlazorAutoMapperProfile()
    {
        CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .Ignore(x => x.Password)
            .Ignore(x => x.RoleNames);

        CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
            .MapExtraProperties();
    }
}
