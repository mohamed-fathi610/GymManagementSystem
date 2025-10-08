using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IHealthRecordRepository
    {
        //GetAllHealthRecords
        IEnumerable<HealthRecord> GetAllHealthRecords();

        //GetById
        HealthRecord? GetById(int id);

        //Add
        int Add(HealthRecord healthRecord);

        //Update
        int Update(HealthRecord healthRecord);

        //Delete
        int Remove(int id);
    }
}
