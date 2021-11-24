using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Evaluation : Entity<Guid>
    {
        public string? ApplicationUserId { get; set; }
        
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUsers { get; set; }
        public string? ApplicationUserIdExpertSoftSkills { get; set; }
        
        [ForeignKey("ApplicationUserIdExpertSoftSkills")]
        public virtual ApplicationUser ApplicationUserExpertSoftSkills { get; set; }
        public string? ApplicationUserIdExpertHardSkills { get; set; }
        
        [ForeignKey("ApplicationUserIdExpertHardSkills")]
        public virtual ApplicationUser ApplicationUserExpertHardSkills { get; set; }
        public string? ApplicationUserIdExpertEnglishSkills { get; set; }
        
        [ForeignKey("ApplicationUserIdExpertEnglishSkills")]
        public virtual ApplicationUser ApplicationUserExpertEnglishSkills { get; set; }
        
        public EvaluationState EvaluationState { get; set; }
        public DateTime DateOfEvaluation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
        public int? CurrentGradeId { get; set; }
        [ForeignKey("CurrentGradeId")]
        public virtual Grade CurrentGrade { get; set; }
        
        public int? NextGradeId { get; set; }
        [ForeignKey("NextGradeId")]
        public virtual Grade NextGrade { get; set; }
        
        public EvaluationSuccessState SoftSkillSuccess { get; set; }
        public EvaluationSuccessState HardSkillSuccess { get; set; }
        public EvaluationSuccessState EnglishSkillSuccess { get; set; }

    }
}