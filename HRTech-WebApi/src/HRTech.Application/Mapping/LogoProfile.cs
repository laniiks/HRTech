using System;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class LogoProfile : Profile
    {
        public LogoProfile()
        {
            CreateMap<Image, FileDto>();
            CreateMap<FileDto, Image>()
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(m=>DateTime.UtcNow));
        }
    }
}