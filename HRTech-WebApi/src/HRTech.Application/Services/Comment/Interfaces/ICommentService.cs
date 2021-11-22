using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Services.Comment.Interfaces
{
    public interface ICommentService
    {
        Task<Guid> Create(Guid evaluationId, ApplicationUser user, CommentDto commentDto,
            CancellationToken cancellationToken);

        Task<ICollection<CommentDto>> GetAllCommentByEvaluation(Guid evaluationId, CancellationToken cancellationToken);
        Task<bool> Delete(Guid commentId, ApplicationUser user, CancellationToken cancellationToken);
    }
}