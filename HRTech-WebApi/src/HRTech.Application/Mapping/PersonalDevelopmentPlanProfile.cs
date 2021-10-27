using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class PersonalDevelopmentPlanProfile : Profile
    {
        public PersonalDevelopmentPlanProfile()
        {
            CreateMap<PersonalDevelopmentPlan, PersonalDevelopmentPlanDto>();
            CreateMap<PersonalDevelopmentPlanDto, PersonalDevelopmentPlan>()
                .ForMember(d => d.ApplicationUser, opt => opt.Ignore());
        }
    }
}