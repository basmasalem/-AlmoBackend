using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{

    public interface IProblemService
    {

        public Problem GetProblemData(int Id);
     
        public void DeleteProblem(Problem Model);
        public void UpdateProblem(Problem Model);
        public Problem AddProblem(Problem Model);
        public List<Problem> GetAllProblems(string Name="",string Email="");


    }
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _ProblemRepository;
        public ProblemService(IProblemRepository ProblemRepository)
        {
            _ProblemRepository = ProblemRepository;
        }
        public Problem AddProblem(Problem Model)
        {
            return _ProblemRepository.Add(Model);
        }

        public void DeleteProblem(Problem Problem)
        {
            _ProblemRepository.Delete(Problem);
        }

   

        public Problem GetProblemData(int Id)
        {
            return _ProblemRepository.List().FirstOrDefault(h=>h.ProblemId==Id);
        }

        public List<Problem> GetAllProblems(string Name="" ,string Email="")
        {
            return _ProblemRepository.List().Where(h=>(string.IsNullOrEmpty(Email) ||h.UserCreated.Email==Email)&&(string.IsNullOrEmpty(Name) || h.UserCreated.Name==Name) && h.IsDeleted!=true).ToList();
        }



        public void UpdateProblem(Problem Model)
        {
            _ProblemRepository.Update(Model);
        }


    }
}
