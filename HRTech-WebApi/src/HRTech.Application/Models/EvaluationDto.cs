using System;
using Common.Enums;
using HRTech.Domain;

namespace HRTech.Application.Models
{
    public class EvaluationDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? ApplicationUserId { get; set; }
        public virtual UserDto ApplicationUsers { get; set; }
        public string? ApplicationUserIdExpertSoftSkills { get; set; }
        public virtual UserDto ApplicationUserExpertSoftSkills { get; set; }
        public string? ApplicationUserIdExpertHardSkills { get; set; }
        public virtual UserDto ApplicationUserExpertHardSkills { get; set; }
        public string? ApplicationUserIdExpertEnglishSkills { get; set; }
        public virtual UserDto ApplicationUserExpertEnglishSkills { get; set; }
        
        public EvaluationState EvaluationState { get; set; }
        public DateTime DateOfEvaluation { get; set; }
        
        public int? CurrentGradeId { get; set; }
        public virtual GradeDto CurrentGrade { get; set; }
        
        public int? NextGradeId { get; set; }
        public virtual GradeDto NextGrade { get; set; }

    }
    
}