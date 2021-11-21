using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.Grade.Interfaces;

namespace HRTech.Application.Services.Grade.Implementations
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeService(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<GradeDto> GetByIdGrade(int id, CancellationToken cancellationToken)
        {
            var grade = await _gradeRepository.GetById(id, cancellationToken);
            if (grade == null)
            {
                return null;
            }

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<ICollection<Domain.Grade>> GetAllGradeInCompany(Guid idCompany, CancellationToken cancellationToken)
        {
            var grades = _mapper.Map<ICollection<GradeDto>>(await _gradeRepository.GetAllGradeInCompany(idCompany, cancellationToken));
            return _mapper.Map<ICollection<Domain.Grade>>(grades);
        }
    }
}