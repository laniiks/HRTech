using System;

namespace HRTech.WebApi.Models.Evaluation
{
    public class CreateEvaluationRequest
    {
        public DateTime DateOfEvaluation { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserIdExpertSoftSkills { get; set; }
        public string ApplicationUserIdExpertHardSkills { get; set; }
        public string ApplicationUserIdExpertEnglishSkills { get; set; }
    }
}