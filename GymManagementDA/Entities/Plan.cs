﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        #region RelationShips

        #region Plan - MemberShips
        public ICollection<MemberShip> MemberShips { get; set; }
        #endregion

        #endregion
    }
}
