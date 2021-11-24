using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Application.Services.Comment.Interfaces;
using HRTech.Domain;
using HRTech.WebApi.Models.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Comment
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(UserManager<ApplicationUser> userManager, IMapper mapper, ICommentService commentService) : base(mapper, userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _commentService = commentService;
        }
        
        [HttpGet("List/{evaluationId:Guid}")]
        public async Task<IActionResult> GetAllCommentByEvaluation(Guid evaluationId, CancellationToken cancellationToken)
        {
            var result = await _commentService.GetAllCommentByEvaluation(evaluationId, cancellationToken);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(
            [Required][FromQuery]Guid evaluationId, 
            [FromBody]CommentCreateRequest commentCreateRequest,
            CancellationToken cancellationToken)
        {
            var result = await _commentService
                .Create(evaluationId, await GetCurrentUser(),_mapper.Map<CommentDto>(commentCreateRequest), cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _commentService.Delete(id, await GetCurrentUser(),cancellationToken);
            return Ok(result);
        }
    }
}