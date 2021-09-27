using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model { 
    public class CourseRepository : ICourseRepository
    {
        public AppDBContext _appDBContext;
        public CourseRepository(AppDBContext appDBContext)
        {
            _appDBContext=appDBContext;
        }
        public Course Add(Course entity)
        {
           _appDBContext.Courses.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            _appDBContext.SaveChanges();
        }

        public void Delete(Course entity)
        {
           
            Update(entity);
            Commit();
        }

        public Course Find(int id)
        {
            return _appDBContext.Courses.Find(id);
        }

        public IQueryable<Course> List()
        {
            return _appDBContext.Courses;
        }

        public void Update(Course entity)
        {
            _appDBContext.Courses.Update(entity);
            Commit();
        }

        
    }
    public interface ICourseRepository : IRepository<Course>
    {
       
    }
}
