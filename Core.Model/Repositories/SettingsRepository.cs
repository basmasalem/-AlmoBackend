using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class SettingsRepository : ISettingsRepository
    {
        public AppDBContext _appDBContext;
        public SettingsRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public Settings Add(Settings entity)
        {
           _appDBContext.Settings.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(Settings entity)
        {
            //entity.IsDeleted = true;
            Update(entity);
            Commit();
        }

        public Settings Find(int id)
        {
            return _appDBContext.Settings.Find(id);
        }

        public IQueryable<Settings> List()
        {
            return _appDBContext.Settings;
        }

        public void Update(Settings entity)
        {
            _appDBContext.Settings.Update(entity);
            Commit();
        }

        
    }
    public interface ISettingsRepository : IRepository<Settings>
    {
       
    }
}
