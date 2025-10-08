using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class MemberShipConfigurations : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder
                .Property(ms => ms.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(m => new { m.MemberId, m.PlanId });

            builder.Ignore(ms => ms.Id);
            builder.Ignore(ms => ms.Status);
        }
    }
}
