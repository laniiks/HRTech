using System;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>()
                .ForMember(d=>d.Companies, opt=>opt.Ignore())
                .ForMember(dest => dest.UpdateDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(m=>DateTime.UtcNow));
            
        }
    }
}