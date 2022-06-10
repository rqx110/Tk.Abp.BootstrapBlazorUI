using AutoMapper;
using Volo.Abp.SettingManagement;

namespace Tk.Abp.SettingManagement.Blazor.BootstrapBlazorUI;

public class SettingManagementBlazorAutoMapperProfile : Profile
{
    public SettingManagementBlazorAutoMapperProfile()
    {
        CreateMap<EmailSettingsDto, UpdateEmailSettingsDto>();
    }
}
