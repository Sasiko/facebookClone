using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookCloneApp.Common
{
    class UnitOfWork : IUnitOfWork
    {
        private FaceBookCloneEntities dbContext;
        public UnitOfWork(FaceBookCloneEntities _dbContext)
        {
            dbContext = _dbContext;
        }
        public FaceBookRepository<TEntity> Repo<TEntity>() where TEntity :class
        {
            return new FaceBookRepository<TEntity>(dbContext);
        }
        public UserRepository<TEntity> UserRepo<TEntity>() where TEntity :class
        {
            return new UserRepository<TEntity>(dbContext);
        }
        public UserPostRepository<TEntity> UserPostRepo<TEntity>() where TEntity :class
        {
            return new UserPostRepository<TEntity>(dbContext);
        }
        public UserCommentsRepository<TEntity> UserCommentsRepo<TEntity>() where TEntity :class
        {
            return new UserCommentsRepository<TEntity>(dbContext);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
