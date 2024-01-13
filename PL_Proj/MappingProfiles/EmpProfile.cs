using AutoMapper;
using DAL_Proj.Entities;
using PL_Proj.ViewModels;

namespace PL_Proj.MappingProfiles
{
    public class EmpProfile : Profile
    {
        public EmpProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
