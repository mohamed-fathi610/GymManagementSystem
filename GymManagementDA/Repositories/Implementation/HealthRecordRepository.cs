using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(HealthRecord healthRecord)
        {
            _DbContext.HealthRecords.Add(healthRecord);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<HealthRecord> GetAllHealthRecords() => _DbContext.HealthRecords.ToList();

        public HealthRecord? GetById(int id) => _DbContext.HealthRecords.Find(id);

        public int Remove(int id)
        {
            var healthRecord = _DbContext.HealthRecords.Find(id);
            if (healthRecord is not null)
            {
                _DbContext.HealthRecords.Remove(healthRecord);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(HealthRecord healthRecord)
        {
            var existingRecord = _DbContext.HealthRecords.Find(healthRecord.Id);
            if (existingRecord is not null)
            {
                _DbContext.HealthRecords.Update(existingRecord);
                return _DbContext.SaveChanges();
            }
            return 0;
        }
    }
}
