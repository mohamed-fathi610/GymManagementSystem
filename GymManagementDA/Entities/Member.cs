namespace GymManagementDAL.Entities
{
    public class Member : GymUser
    {
        //JoinDate => CreatedAt
        public string? Photo { get; set; }

        #region RelationShips

        #region Member - HealthRecord
        public HealthRecord HealthRecord { get; set; }
        #endregion

        #region Member - MemberShips
        public ICollection<MemberShip> MemberShips { get; set; }
        #endregion

        #region Member - Session
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion

        #endregion
    }
}
