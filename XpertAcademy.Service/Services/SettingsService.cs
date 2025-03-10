using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;

namespace XpertAcademy.Service.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SettingsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Settings> GetSettingsAsync()
        {
            var settings = await _unitOfWork.Repository<Settings>().GetAllAsync();

            if (settings == null || !settings.Any())
                throw new Exception("There's No Settings !");

            return settings.FirstOrDefault();
        }

        public async Task<Settings> UpdateSettingsAsync(Settings dto)
        {
            var settings = await _unitOfWork.Repository<Settings>().GetAllAsync();

            var setting = settings.FirstOrDefault();


            if (setting == null)
            {

                setting = new Settings
                {
                    TraineesCount = dto.TraineesCount,
                    TrainersCount = dto.TrainersCount,
                    ProjectsCount = dto.ProjectsCount,
                    CoursesCount = dto.CoursesCount,

                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,

                    AddressAR = dto.AddressAR,
                    AddressEN = dto.AddressEN,

                    FacebookAccount = dto.FacebookAccount,
                    InstagramAccount = dto.InstagramAccount,
                    LinkedInAccount = dto.LinkedInAccount,
                    TiktokAccount = dto.TiktokAccount
                };


                _unitOfWork.Repository<Settings>().Add(setting);
            }
            else
            {
                setting.TraineesCount = dto.TraineesCount;
                setting.TrainersCount = dto.TrainersCount;
                setting.ProjectsCount = dto.ProjectsCount;
                setting.CoursesCount = dto.CoursesCount;

                setting.PhoneNumber = dto.PhoneNumber;
                setting.Email = dto.Email;

                setting.AddressAR = dto.AddressAR;
                setting.AddressEN = dto.AddressEN;


                setting.FacebookAccount = dto.FacebookAccount;
                setting.InstagramAccount = dto.InstagramAccount;
                setting.LinkedInAccount = dto.LinkedInAccount;
                setting.TiktokAccount = dto.TiktokAccount;

                _unitOfWork.Repository<Settings>().Update(setting);
            }

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an Error while update or create Settings");

            return setting;
        }
    }
}
