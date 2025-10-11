using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    public class Trainer : GymUser
    {
        // HireDate => CreatedAt

        public Specialities Specialities { get; set; }
        #region RelationShips
        #region Trainer - Session
        public ICollection<Session> Sessions { get; set; }
        #endregion

        #endregion
    }
}
