using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Implementation;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        public IMemberShipRepository MemberShipRepository { get; }

        public IMemberSessionRepository MemberSessionRepository { get; }

        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity, new();

        int SaveChanges();
    }
}
