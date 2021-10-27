using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.PDP.Interfaces;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using Microsoft.Extensions.Logging;

namespace HRTech.Application.Services.PDP.Implementations
{
    public class PersonalDeveloperPlanService : IPersonalDeveloperPlanService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPersonalDevelopmentPlanRepository _personalDevelopmentPlanRepository;
        private readonly ILogger _logger;

        public PersonalDeveloperPlanService(
            IMapper mapper, 
            IUserService userService, 
            ILogger<PersonalDeveloperPlanService> logger, 
            IPersonalDevelopmentPlanRepository personalDevelopmentPlanRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
            _personalDevelopmentPlanRepository = personalDevelopmentPlanRepository;
        }

        public async Task<Guid> AddPdpdForUser(ApplicationUser user, PersonalDevelopmentPlanDto personalDevelopmentPlanDto,
            CancellationToken cancellationToken)
        {
            try
            {
                var pdp = _mapper.Map<PersonalDevelopmentPlan>(personalDevelopmentPlanDto);
                var users = await _userService.GetUserById(user.Id);
                if (users == null)
                {
                    throw new Exception($"Пользователь с Id {user.Id} не найден.");
                }
                users.PersonalDevelopmentPlans.Add(pdp);
                await _personalDevelopmentPlanRepository.Add(pdp, cancellationToken);
                await _personalDevelopmentPlanRepository.SaveChanges(cancellationToken);
                return pdp.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Some error");
                throw new Exception("Some error", e);
            }
        }

        public async Task<ICollection<PersonalDevelopmentPlanDto>> GetAllPdpForUser(ApplicationUser user, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUserById(user.Id);
            if (users == null)
            {
                throw new Exception($"Пользователь с Id {user.Id} не найден.");
            }

            return _mapper.Map<ICollection<PersonalDevelopmentPlanDto>>(users.PersonalDevelopmentPlans);
        }

        public async Task<PersonalDevelopmentPlanDto> GetFileAsync(Guid fileGuid)
        {
            var file = await _personalDevelopmentPlanRepository.GetByFileGuid(fileGuid);

            return _mapper.Map<PersonalDevelopmentPlanDto>(file);
        }
    }
}