using AutoMapper;
using Route.C41.G01.DAL.Models;
using Route.C41.G01.PL.ViewModels;

namespace Route.C41.G01.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            //.ForMember(d=>d.Name, o=> o.MapFrom(s=> s.EmpName));

            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
