using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Mapping
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(src => $"{src.ApplicationUser.LastName} {src.ApplicationUser.FirstName}"));
            CreateMap<CommentDto, Comment>()
                .ForMember(d => d.Evaluation, opt => opt.Ignore())
                .ForMember(d => d.ApplicationUser, opt => opt.Ignore());
        }
    }
}