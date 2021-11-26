using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Infrastructure.UsersFromExcelFile
{
    public class GetUsersFromExcelFile : IGetUsersFromExcelFile
    {
        private readonly IGeneratePassword _generatePassword;

        public GetUsersFromExcelFile(IGeneratePassword generatePassword)
        {
            _generatePassword = generatePassword;
        }

        List<RegisterDto> IGetUsersFromExcelFile.GetUsersFromExcelFile(ExcelFileUsers companyExcelFileUsers, Guid companyId)
        {
            List<RegisterDto> users = new List<RegisterDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new MemoryStream(companyExcelFileUsers.Content)) 
            using (var reader = ExcelReaderFactory.CreateReader(stream)) 
            {
                while (reader.Read()) 
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (reader != null && reader.GetValue(0) != null)
                    {
                        users.Add(new RegisterDto
                        {
                            FirstName = reader.GetValue(0).ToString(),
                            LastName = reader.GetValue(1).ToString(),
                            Patronymic = reader.GetValue(2).ToString(),
                            Email = reader.GetValue(3).ToString(),
                            PhoneNumber = reader.GetValue(4).ToString(),
                            CompanyId = companyId,
                            Password = _generatePassword.GeneratePassword(),
                            ExpertUserState = reader.GetValue(5).ToString(),
                            IsDirector = reader.GetValue(6).ToString()
                        });
                    }
                    else
                    {
                        break;
                    }
                }
            }
            var itemToRemove = users.First();
            if (itemToRemove != null)
            {
                users.Remove(itemToRemove);
            }
                

            return users;
        }    
    }
}