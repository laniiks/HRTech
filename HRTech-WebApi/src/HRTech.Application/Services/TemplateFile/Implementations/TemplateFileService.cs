using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.TemplateFile.Interfaces;
using HRTech.Domain;

namespace HRTech.Application.Services.TemplateFile.Implementations
{
    public class TemplateFileService : ITemplateFileService
    {
        private readonly IMapper _mapper;
        private IRepository<FileTemplate> _fileTemplateRepository;

        public TemplateFileService(IMapper mapper, IRepository<FileTemplate> fileTemplateRepository)
        {
            _mapper = mapper;
            _fileTemplateRepository = fileTemplateRepository;
        }

        public async Task<int> CreateTemplateFile(FileDto fileDto, CancellationToken cancellationToken)
        {
            var templateFile = _mapper.Map<FileTemplate>(fileDto);
            if (fileDto == null)
            {
                throw new Exception("Файл не найден");
            }

            await _fileTemplateRepository.Add(templateFile, cancellationToken);
            await _fileTemplateRepository.SaveChanges(cancellationToken);
            return templateFile.Id;
        }

        public async Task<FileDto> DownloadTemplateFile(int id, CancellationToken cancellationToken)
        {
            var file = await _fileTemplateRepository.GetById(id, cancellationToken);
            return new FileDto
            {
                FileName = file.FileName,
                Content = file.Content
            };
        }

        public async Task<FileTemplate> GetLastTemplateFile(CancellationToken cancellationToken)
        {
            var templateFile = _mapper.Map<ICollection<FileTemplate>>(await _fileTemplateRepository.GetAll(cancellationToken));
            if (templateFile == null)
            {
                throw new Exception("Шаблон списка сотрудников отсутствует");
            }

            var result = templateFile?.SingleOrDefault(x => x.CreatedDateTime == templateFile.Max(m => m.CreatedDateTime));
            if (result == null)
            {
                throw new Exception("Шаблон списка сотрудников не найден");
            }
            return new FileTemplate
            {
                Id = result.Id,
                FileName = result.FileName,
                Content = result.Content,
                CreatedDateTime = result.CreatedDateTime
            };
        }
    }
}