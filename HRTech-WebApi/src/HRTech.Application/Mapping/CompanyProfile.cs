using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>()
                .ForMember(d => d.Employees, opt => opt.Ignore())
                .ForMember(d => d.Evaluations, opt => opt.Ignore());
        }
    }
}