using AutoMapper;
using DAL_Proj.Entities;
using PL_Proj.ViewModels;

namespace PL_Proj.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserViewModel>().ReverseMap();
        }
    }
}
