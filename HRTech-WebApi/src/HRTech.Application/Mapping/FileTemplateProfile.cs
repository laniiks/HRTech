using System;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class FileTemplateProfile : Profile
    {
        public FileTemplateProfile()
        {
            CreateMap<FileTemplateDto, FileTemplate>()
                .ForMember(d=>d.Id, opt=>opt.Ignore())
                .ForMember(d=>d.CreatedDateTime, opt=>opt.MapFrom(m=>DateTime.UtcNow));

            CreateMap<FileTemplate, FileTemplateDto>();
            
            CreateMap<FileTemplate, FileDto>()
                .ForMember(dest => dest.CompanyId, opt => opt.Ignore())
                .ForMember(dest => dest.FileType, opt => opt.Ignore())
                .ForMember(dest => dest.FileGuid, opt => opt.Ignore());

            CreateMap<FileDto, FileTemplate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(m=>DateTime.UtcNow));

        }
    }
}