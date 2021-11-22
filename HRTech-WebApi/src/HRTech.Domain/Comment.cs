using System;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Comment : Entity<Guid>
    {
        public string TextComment { get; set; }
        
        public Guid EvaluationId { get; set; }
        public virtual Evaluation Evaluation { get; set; }
        
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}