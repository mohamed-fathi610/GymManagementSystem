using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(50)");

            builder.Property(p => p.Description).HasColumnType("varchar(200)");

            builder.Property(p => p.Price).HasPrecision(10, 2);

            builder.ToTable(T =>
                T.HasCheckConstraint("CK_Plan_DurationDays", "DurationDays between 1 and 365")
            );
        }
    }
}
