using System;
using System.Collections.Generic;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface IGetUsersFromExcelFile
    {
        List<RegisterDto> GetUsersFromExcelFile(ExcelFileUsers companyExcelFileUsers, Guid companyId);
    }
}