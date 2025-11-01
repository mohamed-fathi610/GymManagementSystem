using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Implementation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork.Interfaces;

namespace GymManagementDAL.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;
        public ISessionRepository SessionRepository { get; }
        public IMemberShipRepository MemberShipRepository { get; }
        public IMemberSessionRepository MemberSessionRepository { get; }

        public UnitOfWork(
            GymDbContext dbContext,
            ISessionRepository sessionRepository,
            IMemberShipRepository memberShipRepository,
            IMemberSessionRepository memberSessionRepository
        )
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepository;
            MemberShipRepository = memberShipRepository;
            MemberSessionRepository = memberSessionRepository;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
                return (IGenericRepository<TEntity>)repository;

            var newRepository = new GenericRepository<TEntity>(_dbContext);
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
