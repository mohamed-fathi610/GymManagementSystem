﻿namespace GymManagementDAL.Entities
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }

        // UpdatedAt => LastUpdated
    }
}
