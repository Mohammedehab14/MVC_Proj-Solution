using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PL_Proj.ViewModels;

namespace PL_Proj.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
        CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(R => R.RoleName, O => O.MapFrom(S => S.Name))
                .ReverseMap();
        }
    }
}
