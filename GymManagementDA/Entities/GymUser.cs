using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Entities
{
    internal abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public Adress Adress { get; set; } = null!;
    }

    [Owned]
    class Adress
    {
        public int BuildingNumber { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
}
