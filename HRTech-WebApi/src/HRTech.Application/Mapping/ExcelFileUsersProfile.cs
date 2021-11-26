using System;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class ExcelFileUsersProfile : Profile
    {
        public ExcelFileUsersProfile()
        {
            CreateMap<ExcelFileUsers, FileDto>()
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(d=>d.ApplicationUserId, o=>o.Ignore());
            CreateMap<FileDto, ExcelFileUsers>()
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(m=>DateTime.UtcNow));
        }
    }
}