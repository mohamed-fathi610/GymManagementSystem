using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate => CreatedAt

        public Specialities Specialities { get; set; }
    }
}
