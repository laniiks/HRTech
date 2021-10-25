using System;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Models;
using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.Company;
using HRTech.WebApi.Models.File;

namespace HRTech.WebApi.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateCompanyRequest, CompanyDto>()
                .ForMember(d=>d.Id, o=>o.Ignore())
                .ForMember(d=>d.CreatedDateTime, o=>o.MapFrom(m=>DateTime.UtcNow))
                .ForMember(d=>d.State, o=>o.MapFrom(m=>CompanyState.New))
                .ForMember(d=>d.Employees, o=>o.Ignore())
                .ForMember(d=>d.UpdateDateTime, o=>o.Ignore());
            CreateMap<EditCompanyRequest, CompanyDto>()
                .ForMember(d=>d.State, o=>o.Ignore())
                .ForMember(d=>d.Employees, o=>o.Ignore())
                .ForMember(d=>d.CreatedDateTime, o=>o.Ignore())
                .ForMember(d=>d.UpdateDateTime, o=>o.MapFrom(m=>DateTime.UtcNow));
            CreateMap<AddressCreateRequest, AddressDto>()
                .ForMember(d=>d.Id, o=>o.Ignore())
                .ForMember(d=>d.CreatedDateTime, o=>o.Ignore());
            CreateMap<UploadFileRequest, FileDto>()
                .ForMember(d=>d.CompanyId, o=>o.Ignore());
        }
    }
}