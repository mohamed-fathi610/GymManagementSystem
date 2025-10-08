using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(Trainer trainer)
        {
            _DbContext.Trainers.Add(trainer);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers() => _DbContext.Trainers.ToList();

        public Trainer? GetById(int id) => _DbContext.Trainers.Find(id);

        public int Remove(int id)
        {
            var trainer = _DbContext.Trainers.Find(id);
            if (trainer is not null)
            {
                _DbContext.Trainers.Remove(trainer);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(Trainer trainer)
        {
            _DbContext.Trainers.Update(trainer);
            return _DbContext.SaveChanges();
        }
    }
}
