using System;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Models;
using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.Comment;
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
                .ForMember(d=>d.UpdateDateTime, o=>o.Ignore())
                .ForMember(d=>d.GradesCollection, opt=>opt.MapFrom(m=>m.Grades));
            CreateMap<GradeCreateRequest, GradeDto>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CompanyId, o => o.Ignore())
                .ForMember(d => d.CreatedDateTime, o => o.Ignore());

            CreateMap<EditCompanyRequest, CompanyDto>()
                .ForMember(d=>d.State, o=>o.Ignore())
                .ForMember(d=>d.Employees, o=>o.Ignore())
                .ForMember(d=>d.CreatedDateTime, o=>o.Ignore())
                .ForMember(d=>d.UpdateDateTime, o=>o.MapFrom(m=>DateTime.UtcNow))
                .ForMember(d=>d.GradesCollection, o=>o.Ignore());
            CreateMap<AddressCreateRequest, AddressDto>()
                .ForMember(d=>d.Id, o=>o.Ignore())
                .ForMember(d=>d.CreatedDateTime, o=>o.Ignore());
            CreateMap<UploadFileRequest, FileDto>()
                .ForMember(d=>d.CompanyId, o=>o.Ignore())
                .ForMember(d=>d.ApplicationUserId, o=>o.Ignore());
            
            CreateMap<CommentCreateRequest, CommentDto>()
                .ForMember(d => d.UserName, opt => opt.Ignore())
                .ForMember(d => d.ApplicationUserId, opt => opt.Ignore())
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.EvaluationId, opt => opt.Ignore())
                .ForMember(d => d.CreatedDateTime, opt => opt.MapFrom(m => DateTime.UtcNow));
        }
    }
}