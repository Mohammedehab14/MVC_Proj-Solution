using AutoMapper;
using DAL_Proj.Entities;
using PL_Proj.ViewModels;

namespace PL_Proj.MappingProfiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
