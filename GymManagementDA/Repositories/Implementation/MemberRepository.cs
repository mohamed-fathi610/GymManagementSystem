using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Implementation
{
    internal class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _DbContext = new GymDbContext();

        public int Add(Member member)
        {
            _DbContext.Members.Add(member);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAllMembers() => _DbContext.Members.ToList();

        public Member? GetById(int id) => _DbContext.Members.Find(id);

        public int Remove(int id)
        {
            var member = GetById(id);
            if (member is not null)
            {
                _DbContext.Members.Remove(member);
                return _DbContext.SaveChanges();
            }
            return 0;
        }

        public int Update(Member member)
        {
            var existingMember = GetById(member.Id);
            if (existingMember is not null)
            {
                _DbContext.Members.Update(member);
                return _DbContext.SaveChanges();
            }
            return 0;
        }
    }
}
