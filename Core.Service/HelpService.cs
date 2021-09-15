using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{

    public interface IHelpService
    {

        public Help GetHelpData(int Id);
     
        public void DeleteHelp(Help Model);
        public void UpdateHelp(Help Model);
        public Help AddHelp(Help Model);
        public List<Help> GetAllHelps(string Name="",string Email="");


    }
    public class HelpService : IHelpService
    {
        private readonly IHelpRepository _HelpRepository;
        public HelpService(IHelpRepository HelpRepository)
        {
            _HelpRepository = HelpRepository;
        }
        public Help AddHelp(Help Model)
        {
            return _HelpRepository.Add(Model);
        }

        public void DeleteHelp(Help Help)
        {
            _HelpRepository.Delete(Help);
        }

   

        public Help GetHelpData(int Id)
        {
            return _HelpRepository.List().FirstOrDefault(h=>h.HelpId==Id);
        }

        public List<Help> GetAllHelps(string Name="" ,string Email="")
        {
            return _HelpRepository.List().Where(h=>(string.IsNullOrEmpty(Email) ||h.Email==Email)&&(string.IsNullOrEmpty(Name) || h.Name==Name) && h.IsDeleted!=true).ToList();
        }



        public void UpdateHelp(Help Model)
        {
            _HelpRepository.Update(Model);
        }


    }
}
