using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.Comment.Interfaces;
using HRTech.Domain;

namespace HRTech.Application.Services.Comment.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Comment> _commentRepository;
        private readonly IEvaluationRepository _evaluationRepository;

        public CommentService(IMapper mapper, IRepository<Domain.Comment> commentRepository, IEvaluationRepository evaluationRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _evaluationRepository = evaluationRepository;
        }

        public async Task<Guid> Create(Guid evaluationId, ApplicationUser user, CommentDto commentDto, CancellationToken cancellationToken)
        {
            try
            {
                var comment = _mapper.Map<Domain.Comment>(commentDto);
                var evaluation = await _evaluationRepository.GetByIdGuid(evaluationId);
                if (evaluation == null)
                {
                    throw new Exception($"Оценка с Id [{evaluationId}] не было найдено.");
                }

                evaluation.Comments.Add(comment);
                await _commentRepository.Add(comment, cancellationToken);
                
                user.Comments.Add(comment);
                
                await _evaluationRepository.SaveChanges(cancellationToken);
                await _commentRepository.SaveChanges(cancellationToken);
                return comment.Id;
            }
            catch (Exception e)
            {
                throw new Exception("Error", e);
            }
        }

        public async Task<ICollection<CommentDto>> GetAllCommentByEvaluation(Guid evaluationId, CancellationToken cancellationToken)
        {
            var advert = await _evaluationRepository.GetByIdGuid(evaluationId);
            if (advert == null)
            {
                throw new Exception($"Оценка с Id [{evaluationId}] не было найдено.");
            }
            return _mapper.Map<ICollection<CommentDto>>(advert.Comments);        
        }

        public async Task<bool> Delete(Guid commentId, ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                var comment = await _commentRepository.GetByIdGuid(commentId);
                
                if (comment.ApplicationUserId != user.Id)
                {
                    throw new Exception("Нет прав на удаление комментария.");
                }
                
                
                await _commentRepository.Delete(comment, cancellationToken);
                await _commentRepository.SaveChanges(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }

            return true;
        }
    }
}