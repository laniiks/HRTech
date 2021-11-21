using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class GradeProfile: Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeDto>();
            CreateMap<GradeDto, Grade>()
                .ForMember(d=>d.ApplicationUser, opt=>opt.Ignore())
                .ForMember(d=>d.Company, opt=>opt.Ignore());

        }
    }
}