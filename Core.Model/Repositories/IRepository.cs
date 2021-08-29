using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
  public interface IRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Commit();
    }
}
