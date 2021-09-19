using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class ProblemRepository : IProblemRepository
    {
        public AppDBContext _appDBContext;
        public ProblemRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public Problem Add(Problem entity)
        {
           _appDBContext.Problems.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(Problem entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            Commit();
        }

        public Problem Find(int id)
        {
            return _appDBContext.Problems.Find(id);
        }

        public IQueryable<Problem> List()
        {
            return _appDBContext.Problems.OrderByDescending(x=>x.DateCreated);
        }

        public void Update(Problem entity)
        {
            _appDBContext.Problems.Update(entity);
            Commit();
        }

        
    }
    public interface IProblemRepository : IRepository<Problem>
    {
       
    }
}
