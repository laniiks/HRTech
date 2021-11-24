using System;

namespace HRTech.Application.Models
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string TextComment { get; set; }
        public Guid EvaluationId { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
    }
}