using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class SubscribeRequestRepository : ISubscribeRequestRepository
    {
        public AppDBContext _appDBContext;
        public SubscribeRequestRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public SubscribeRequest Add(SubscribeRequest entity)
        {
           _appDBContext.SubscribeRequests.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(SubscribeRequest entity)
        {
            entity.IsDeleted = true;
            Update(entity);
            Commit();
        }

        public SubscribeRequest Find(int id)
        {
            return _appDBContext.SubscribeRequests.Find(id);
        }

        public IQueryable<SubscribeRequest> List()
        {
            return _appDBContext.SubscribeRequests.Where(u => u.IsDeleted != true).OrderByDescending(x=>x.DateCreated);
        }

        public void Update(SubscribeRequest entity)
        {
            _appDBContext.SubscribeRequests.Update(entity);
            Commit();
        }

        
    }
    public interface ISubscribeRequestRepository : IRepository<SubscribeRequest>
    {
       
    }
}
