using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class HelpRepository : IHelpRepository
    {
        public AppDBContext _appDBContext;
        public HelpRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public Help Add(Help entity)
        {
           _appDBContext.Help.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(Help entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            Commit();
        }

        public Help Find(int id)
        {
            return _appDBContext.Help.Find(id);
        }

        public IQueryable<Help> List()
        {
            return _appDBContext.Help.OrderByDescending(x=>x.DateCreated);
        }

        public void Update(Help entity)
        {
            _appDBContext.Help.Update(entity);
            Commit();
        }

        
    }
    public interface IHelpRepository : IRepository<Help>
    {
       
    }
}
