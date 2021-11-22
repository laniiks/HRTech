using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.Evaluation.Interfaces;
using HRTech.Domain;

namespace HRTech.Application.Services.Evaluation.Implementations
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IMapper _mapper;
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly IGradeRepository _gradeRepository;

        public EvaluationService(IMapper mapper, IEvaluationRepository evaluationRepository, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
            _gradeRepository = gradeRepository;
        }


        public async Task<EvaluationDto> GetByIdEvaluation(Guid evaluationId, CancellationToken cancellationToken)
        {

            var evaluation = await _evaluationRepository.GetByIdGuid(evaluationId);

            return new EvaluationDto
            {
                Id = evaluation.Id,
                CreatedDateTime = evaluation.CreatedDateTime,
                EvaluationState = evaluation.EvaluationState,
                DateOfEvaluation = evaluation.DateOfEvaluation,
                ApplicationUserId = evaluation.ApplicationUserId,
                ApplicationUsers = evaluation.ApplicationUsers != null
                        ? new UserDto
                        {
                            ApplicationUserId = evaluation.ApplicationUserId,
                            FirstName = evaluation.ApplicationUsers.FirstName,
                            LastName = evaluation.ApplicationUsers.LastName,
                            Patronymic = evaluation.ApplicationUsers.Patronymic,
                            ExpertUserState = evaluation.ApplicationUsers.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertSoftSkills = evaluation.ApplicationUserIdExpertSoftSkills,
                    ApplicationUserExpertSoftSkills = evaluation.ApplicationUserExpertSoftSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = evaluation.ApplicationUserIdExpertSoftSkills,
                            FirstName = evaluation.ApplicationUserExpertSoftSkills.FirstName,
                            LastName = evaluation.ApplicationUserExpertSoftSkills.LastName,
                            Patronymic = evaluation.ApplicationUserExpertSoftSkills.Patronymic,
                            ExpertUserState = evaluation.ApplicationUserExpertSoftSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertHardSkills = evaluation.ApplicationUserIdExpertHardSkills,
                    ApplicationUserExpertHardSkills = evaluation.ApplicationUserExpertHardSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = evaluation.ApplicationUserIdExpertHardSkills,
                            FirstName = evaluation.ApplicationUserExpertHardSkills.FirstName,
                            LastName = evaluation.ApplicationUserExpertHardSkills.LastName,
                            Patronymic = evaluation.ApplicationUserExpertHardSkills.Patronymic,
                            ExpertUserState = evaluation.ApplicationUserExpertHardSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertEnglishSkills = evaluation.ApplicationUserIdExpertEnglishSkills,
                    ApplicationUserExpertEnglishSkills = evaluation.ApplicationUserExpertEnglishSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = evaluation.ApplicationUserIdExpertEnglishSkills,
                            FirstName = evaluation.ApplicationUserExpertEnglishSkills.FirstName,
                            LastName = evaluation.ApplicationUserExpertEnglishSkills.LastName,
                            Patronymic = evaluation.ApplicationUserExpertEnglishSkills.Patronymic,
                            ExpertUserState = evaluation.ApplicationUserExpertEnglishSkills.ExpertUserState
                        }
                        : new UserDto(),
                    CurrentGradeId = evaluation.CurrentGradeId,
                    CurrentGrade = evaluation.CurrentGrade != null ? new GradeDto
                    {
                        Title = evaluation.CurrentGrade.Title
                    } : new GradeDto(),
                    NextGradeId = evaluation.NextGradeId,
                    NextGrade = evaluation.NextGrade != null ? new GradeDto
                    {
                        Title = evaluation.NextGrade.Title
                    } : new GradeDto()
            };
        }

        public async Task<GetAll.Response> GetAllResponseEvaluationForUser(ApplicationUser user, CancellationToken cancellationToken)
        {
            var evaluations = await _evaluationRepository.GetAllEvaluationForUser(user.Id, cancellationToken);

            return new GetAll.Response
            {
                Evaluation = evaluations.Select(eva => new EvaluationDto
                {
                    Id = eva.Id,
                    CreatedDateTime = eva.CreatedDateTime,
                    EvaluationState = eva.EvaluationState,
                    DateOfEvaluation = eva.DateOfEvaluation,
                    ApplicationUserId = eva.ApplicationUserId,
                    ApplicationUsers = eva.ApplicationUsers != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserId,
                            FirstName = eva.ApplicationUsers.FirstName,
                            LastName = eva.ApplicationUsers.LastName,
                            Patronymic = eva.ApplicationUsers.Patronymic,
                            ExpertUserState = eva.ApplicationUsers.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertSoftSkills = eva.ApplicationUserIdExpertSoftSkills,
                    ApplicationUserExpertSoftSkills = eva.ApplicationUserExpertSoftSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertSoftSkills,
                            FirstName = eva.ApplicationUserExpertSoftSkills.FirstName,
                            LastName = eva.ApplicationUserExpertSoftSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertSoftSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertSoftSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertHardSkills = eva.ApplicationUserIdExpertHardSkills,
                    ApplicationUserExpertHardSkills = eva.ApplicationUserExpertHardSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertHardSkills,
                            FirstName = eva.ApplicationUserExpertHardSkills.FirstName,
                            LastName = eva.ApplicationUserExpertHardSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertHardSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertHardSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertEnglishSkills = eva.ApplicationUserIdExpertEnglishSkills,
                    ApplicationUserExpertEnglishSkills = eva.ApplicationUserExpertEnglishSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertEnglishSkills,
                            FirstName = eva.ApplicationUserExpertEnglishSkills.FirstName,
                            LastName = eva.ApplicationUserExpertEnglishSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertEnglishSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertEnglishSkills.ExpertUserState
                        }
                        : new UserDto(),
                    CurrentGradeId = eva.CurrentGradeId,
                    CurrentGrade = eva.CurrentGrade != null ? new GradeDto
                    {
                        Title = eva.CurrentGrade.Title
                    } : new GradeDto(),
                    NextGradeId = eva.NextGradeId,
                    NextGrade = eva.NextGrade != null ? new GradeDto
                    {
                        Title = eva.NextGrade.Title
                    } : new GradeDto()
                })
            };
        }

        public async Task<GetAll.Response> GetAllResponseEvaluationForExpertUser(ApplicationUser user, CancellationToken cancellationToken)
        {
            var evaluations = await _evaluationRepository.GetAllEvaluationForExpertUser(user.Id, cancellationToken);

            return new GetAll.Response
            {
                Evaluation = evaluations.Select(eva => new EvaluationDto
                {
                    Id = eva.Id,
                    CreatedDateTime = eva.CreatedDateTime,
                    EvaluationState = eva.EvaluationState,
                    DateOfEvaluation = eva.DateOfEvaluation,
                    ApplicationUserId = eva.ApplicationUserId,
                    ApplicationUsers = eva.ApplicationUsers != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserId,
                            FirstName = eva.ApplicationUsers.FirstName,
                            LastName = eva.ApplicationUsers.LastName,
                            Patronymic = eva.ApplicationUsers.Patronymic,
                            ExpertUserState = eva.ApplicationUsers.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertSoftSkills = eva.ApplicationUserIdExpertSoftSkills,
                    ApplicationUserExpertSoftSkills = eva.ApplicationUserExpertSoftSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertSoftSkills,
                            FirstName = eva.ApplicationUserExpertSoftSkills.FirstName,
                            LastName = eva.ApplicationUserExpertSoftSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertSoftSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertSoftSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertHardSkills = eva.ApplicationUserIdExpertHardSkills,
                    ApplicationUserExpertHardSkills = eva.ApplicationUserExpertHardSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertHardSkills,
                            FirstName = eva.ApplicationUserExpertHardSkills.FirstName,
                            LastName = eva.ApplicationUserExpertHardSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertHardSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertHardSkills.ExpertUserState
                        }
                        : new UserDto(),
                    ApplicationUserIdExpertEnglishSkills = eva.ApplicationUserIdExpertEnglishSkills,
                    ApplicationUserExpertEnglishSkills = eva.ApplicationUserExpertEnglishSkills != null
                        ? new UserDto
                        {
                            ApplicationUserId = eva.ApplicationUserIdExpertEnglishSkills,
                            FirstName = eva.ApplicationUserExpertEnglishSkills.FirstName,
                            LastName = eva.ApplicationUserExpertEnglishSkills.LastName,
                            Patronymic = eva.ApplicationUserExpertEnglishSkills.Patronymic,
                            ExpertUserState = eva.ApplicationUserExpertEnglishSkills.ExpertUserState
                        }
                        : new UserDto(),
                    CurrentGradeId = eva.CurrentGradeId,
                    CurrentGrade = eva.CurrentGrade != null ? new GradeDto
                    {
                        Title = eva.CurrentGrade.Title
                    } : new GradeDto(),
                    NextGradeId = eva.NextGradeId,
                    NextGrade = eva.NextGrade != null ? new GradeDto
                    {
                        Title = eva.NextGrade.Title
                    } : new GradeDto()
                })
            };
        }

        public async Task<Guid> CreateEvaluation(EvaluationDto evaluationDto, ApplicationUser user,
            CancellationToken cancellationToken)
        {
            Domain.Grade grade = null;
            Domain.Evaluation evaluation = null;
            if (user.CompanyId != null && user.GradeId != null)
            {
                grade = await _gradeRepository.GetNextGrade((Guid) user.CompanyId, (int) user.GradeId,
                        cancellationToken);
                evaluation = new Domain.Evaluation
                {
                    CreatedDateTime = DateTime.UtcNow,
                    EvaluationState = EvaluationState.New,
                    DateOfEvaluation = evaluationDto.DateOfEvaluation,
                    ApplicationUserId = user.Id,
                    ApplicationUserIdExpertSoftSkills = evaluationDto.ApplicationUserIdExpertSoftSkills,
                    ApplicationUserIdExpertHardSkills = evaluationDto.ApplicationUserIdExpertHardSkills,
                    ApplicationUserIdExpertEnglishSkills = evaluationDto.ApplicationUserIdExpertEnglishSkills,
                    CurrentGradeId = user.GradeId,
                    NextGradeId = grade.Id
                };
            }
            
            await _evaluationRepository.Add(evaluation, cancellationToken);
            await _evaluationRepository.SaveChanges(cancellationToken);
            return evaluation.Id;
        }
    }
}