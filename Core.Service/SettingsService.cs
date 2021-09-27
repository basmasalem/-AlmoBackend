using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{

    public interface ISettingsService
    {

        public Settings GetSettingsData();
     
        public void DeleteSettings(Settings Model);
        public void UpdateSettings(Settings Model);
        public Settings AddSettings(Settings Model);
        public List<Course> GetAllCourse();



    }
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _SettingsRepository;
        private readonly ICourseRepository _CourseRepository;
        public SettingsService(ISettingsRepository SettingsRepository, ICourseRepository CourseRepository)
        {
            _SettingsRepository = SettingsRepository;
            _CourseRepository = CourseRepository;
        }
        public Settings AddSettings(Settings Model)
        {
            return _SettingsRepository.Add(Model);
        }

        public void DeleteSettings(Settings Settings)
        {
            _SettingsRepository.Delete(Settings);
        }

   

        public Settings GetSettingsData()
        {
            return _SettingsRepository.List().FirstOrDefault();
        }

        public List<Course> GetAllCourse()
        {
            return _CourseRepository.List().ToList();
        }



        public void UpdateSettings(Settings Model)
        {
            _SettingsRepository.Update(Model);
        }


    }
}
