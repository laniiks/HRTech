using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class EvaluationProfile : Profile
    {
        public EvaluationProfile()
        {
            CreateMap<Evaluation, EvaluationDto>();
            CreateMap<EvaluationDto, Evaluation>()
                .ForMember(d=>d.ApplicationUsers, opt=>opt.Ignore())
                .ForMember(d=>d.ApplicationUserExpertHardSkills, opt=>opt.Ignore())
                .ForMember(d=>d.ApplicationUserExpertSoftSkills, opt=>opt.Ignore())
                .ForMember(d=>d.ApplicationUserExpertEnglishSkills, opt=>opt.Ignore())
                ;
        }
    }
}