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
        private readonly IApplicationUserRepository _applicationUserRepository;

        public EvaluationService(IMapper mapper, 
            IEvaluationRepository evaluationRepository, 
            IGradeRepository gradeRepository, 
            IApplicationUserRepository applicationUserRepository)
        {
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
            _gradeRepository = gradeRepository;
            _applicationUserRepository = applicationUserRepository;
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
                            ExpertUserState = evaluation.ApplicationUsers.ExpertUserState,
                            Photo = evaluation.ApplicationUsers.Photo != null ? new FileDto
                            {
                                Content = evaluation.ApplicationUsers.Photo.Content
                            }: null
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
                            ExpertUserState = evaluation.ApplicationUserExpertSoftSkills.ExpertUserState,
                            Photo = evaluation.ApplicationUserExpertSoftSkills.Photo != null ? new FileDto
                            {
                                Content = evaluation.ApplicationUserExpertSoftSkills.Photo.Content
                            }: null
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
                            ExpertUserState = evaluation.ApplicationUserExpertHardSkills.ExpertUserState,
                            Photo = evaluation.ApplicationUserExpertHardSkills.Photo != null ? new FileDto
                            {
                                Content = evaluation.ApplicationUserExpertHardSkills.Photo.Content
                            }: null
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
                            ExpertUserState = evaluation.ApplicationUserExpertEnglishSkills.ExpertUserState,
                            Photo = evaluation.ApplicationUserExpertEnglishSkills.Photo != null ? new FileDto
                            {
                                Content = evaluation.ApplicationUserExpertEnglishSkills.Photo.Content
                            }: null
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
                    } : new GradeDto(),
                    SoftSkillSuccess = evaluation.SoftSkillSuccess,
                    HardSkillSuccess = evaluation.HardSkillSuccess,
                    EnglishSkillSuccess = evaluation.EnglishSkillSuccess,

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
                            ExpertUserState = eva.ApplicationUsers.ExpertUserState,
                            Photo = eva.ApplicationUsers.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUsers.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertSoftSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertSoftSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertSoftSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertHardSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertHardSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertHardSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertEnglishSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertEnglishSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertEnglishSkills.Photo.Content
                            }: null
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
                    } : new GradeDto(),
                    SoftSkillSuccess = eva.SoftSkillSuccess,
                    HardSkillSuccess = eva.HardSkillSuccess,
                    EnglishSkillSuccess = eva.EnglishSkillSuccess,
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
                            ExpertUserState = eva.ApplicationUsers.ExpertUserState,
                            Photo = eva.ApplicationUsers.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUsers.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertSoftSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertSoftSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertSoftSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertHardSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertHardSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertHardSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertEnglishSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertEnglishSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertEnglishSkills.Photo.Content
                            }: null
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
                    } : new GradeDto(),
                    SoftSkillSuccess = eva.SoftSkillSuccess,
                    HardSkillSuccess = eva.HardSkillSuccess,
                    EnglishSkillSuccess = eva.EnglishSkillSuccess,
                })
            }; 
        }

        public async Task<GetAll.Response> GetAllResponseEvaluationInCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var evaluations = await _evaluationRepository.GetAllEvalutionInCompany(companyId, cancellationToken);

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
                            ExpertUserState = eva.ApplicationUsers.ExpertUserState,
                            Photo = eva.ApplicationUsers.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUsers.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertSoftSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertSoftSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertSoftSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertHardSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertHardSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertHardSkills.Photo.Content
                            }: null
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
                            ExpertUserState = eva.ApplicationUserExpertEnglishSkills.ExpertUserState,
                            Photo = eva.ApplicationUserExpertEnglishSkills.Photo != null ? new FileDto
                            {
                                Content = eva.ApplicationUserExpertEnglishSkills.Photo.Content
                            }: null
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
                    } : new GradeDto(),
                    SoftSkillSuccess = eva.SoftSkillSuccess,
                    HardSkillSuccess = eva.HardSkillSuccess,
                    EnglishSkillSuccess = eva.EnglishSkillSuccess,
                })
            };        
        }

        public async Task<Guid> CreateEvaluation(EvaluationDto evaluationDto, ApplicationUser user,
            CancellationToken cancellationToken)
        {
            Domain.Evaluation evaluation = null;
            if (user.CompanyId != null && user.GradeId != null)
            {
                var grade = await _gradeRepository.GetNextGrade((Guid) user.CompanyId, (int) user.GradeId,
                        cancellationToken);
                if (grade == null)
                {
                    grade = new Domain.Grade
                    {
                        Id = (int) user.GradeId
                    };
                }
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
                    NextGradeId = grade.Id,
                    CompanyId = user.CompanyId
                };
            }
            
            await _evaluationRepository.Add(evaluation, cancellationToken);
            await _evaluationRepository.SaveChanges(cancellationToken);
            return evaluation.Id;
        }

        public async Task<Guid> SoftSkillSuccess(Guid evaluationId, EvaluationSuccessState skillSuccess, ApplicationUser user, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdGuid(evaluationId);
            if (evaluation.ApplicationUserIdExpertSoftSkills != user.Id)
            {
                throw new Exception("Вы не являетесь экспертом Soft Skill");
            }
            evaluation.SoftSkillSuccess = skillSuccess;
            if (evaluation.EvaluationState == EvaluationState.New)
            {
                evaluation.EvaluationState = EvaluationState.InProgress;
            }
            
            if (evaluation.EvaluationState == EvaluationState.InProgress)
            {
                evaluation.EvaluationState = CheckSuccessEvaluationAndChangeState(evaluation);
            }            
            await _evaluationRepository.Update(evaluation, cancellationToken);
            await _evaluationRepository.SaveChanges(cancellationToken);
            
            if (evaluation.EvaluationState == EvaluationState.Passed)
            {
                user.GradeId = evaluation.NextGradeId;
                await _applicationUserRepository.Update(user, cancellationToken);
                await _applicationUserRepository.SaveChanges(cancellationToken);
            }
            
            return evaluation.Id;
        }
        
        public async Task<Guid> HardSkillSuccess(Guid evaluationId, EvaluationSuccessState skillSuccess, ApplicationUser user, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdGuid(evaluationId);
            if (evaluation.ApplicationUserIdExpertHardSkills != user.Id)
            {
                throw new Exception("Вы не являетесь экспертом Hard Skill");
            }
            evaluation.HardSkillSuccess = skillSuccess;
            if (evaluation.EvaluationState == EvaluationState.New)
            {
                evaluation.EvaluationState = EvaluationState.InProgress;
            }
            
            if (evaluation.EvaluationState == EvaluationState.InProgress)
            {
                evaluation.EvaluationState = CheckSuccessEvaluationAndChangeState(evaluation);
            }

            
            await _evaluationRepository.Update(evaluation, cancellationToken);
            await _evaluationRepository.SaveChanges(cancellationToken);
            
            if (evaluation.EvaluationState == EvaluationState.Passed)
            {
                user.GradeId = evaluation.NextGradeId;
                await _applicationUserRepository.Update(user, cancellationToken);
                await _applicationUserRepository.SaveChanges(cancellationToken);
            }
            return evaluation.Id;        
        }

        public async Task<Guid> EnglishSkillSuccess(Guid evaluationId, EvaluationSuccessState skillSuccess, ApplicationUser user, CancellationToken cancellationToken)
        {
            var evaluation = await _evaluationRepository.GetByIdGuid(evaluationId);
            if (evaluation.ApplicationUserIdExpertEnglishSkills != user.Id)
            {
                throw new Exception("Вы не являетесь экспертом English");
            }
            evaluation.EnglishSkillSuccess = skillSuccess;
            if (evaluation.EvaluationState == EvaluationState.New)
            {
                evaluation.EvaluationState = EvaluationState.InProgress;
            }

            if (evaluation.EvaluationState == EvaluationState.InProgress)
            {
                evaluation.EvaluationState = CheckSuccessEvaluationAndChangeState(evaluation);
            }
            
            await _evaluationRepository.Update(evaluation, cancellationToken);
            await _evaluationRepository.SaveChanges(cancellationToken);
            
            if (evaluation.EvaluationState == EvaluationState.Passed)
            {
                user.GradeId = evaluation.NextGradeId;
                await _applicationUserRepository.Update(user, cancellationToken);
                await _applicationUserRepository.SaveChanges(cancellationToken);
            }
            
            return evaluation.Id;        
        }
        
        private EvaluationState CheckSuccessEvaluationAndChangeState(Domain.Evaluation evaluation)
        {
            if (evaluation.SoftSkillSuccess == EvaluationSuccessState.Success &&
                evaluation.HardSkillSuccess == EvaluationSuccessState.Success &&
                evaluation.EnglishSkillSuccess == EvaluationSuccessState.Success)
            {
                return EvaluationState.Passed;
            }

            if (evaluation.SoftSkillSuccess == EvaluationSuccessState.Canceled ||
                evaluation.HardSkillSuccess == EvaluationSuccessState.Canceled ||
                evaluation.EnglishSkillSuccess == EvaluationSuccessState.Canceled)
            {
                return EvaluationState.Canceled;
            }

            return evaluation.EvaluationState;
        }
    }
}