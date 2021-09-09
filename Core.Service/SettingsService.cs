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




    }
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _SettingsRepository;
        public SettingsService(ISettingsRepository SettingsRepository)
        {
            _SettingsRepository = SettingsRepository;
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

       
       

        public void UpdateSettings(Settings Model)
        {
            _SettingsRepository.Update(Model);
        }


    }
}
