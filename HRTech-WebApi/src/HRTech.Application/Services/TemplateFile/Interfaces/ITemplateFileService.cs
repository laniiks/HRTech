using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Services.TemplateFile.Interfaces
{
    public interface ITemplateFileService
    {
        Task<int> CreateTemplateFile(FileDto fileDto, CancellationToken cancellationToken);
        Task<FileDto> DownloadTemplateFile(int id, CancellationToken cancellationToken);
        Task<FileTemplate> GetLastTemplateFile(CancellationToken cancellationToken);
    }
}