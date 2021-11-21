using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;

namespace HRTech.Application.Services.Grade.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto> GetByIdGrade(int id, CancellationToken cancellationToken);
        Task<ICollection<Domain.Grade>> GetAllGradeInCompany(Guid idCompany, CancellationToken cancellationToken);
    }
}