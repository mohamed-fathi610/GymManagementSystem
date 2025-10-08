using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class GymUserConfigurations<T> : IEntityTypeConfiguration<T>
        where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(gu => gu.Name).HasColumnType("varchar(50)");

            builder.Property(gu => gu.Email).HasColumnType("varchar(100)");

            builder.ToTable(T => T.HasCheckConstraint("CK_Email", "Email LIKE '_%@_%._%'"));

            builder.HasIndex(gu => gu.Email).IsUnique();

            builder.Property(gu => gu.Phone).HasColumnType("varchar(11)");
            builder.ToTable(T =>
                T.HasCheckConstraint(
                    "Egyption_CK_Phone",
                    "Phone LIKE '01[0125]%' and Phone not LIKE '%[^0-9]%'"
                )
            );
            builder.HasIndex(gu => gu.Phone).IsUnique();

            builder.OwnsOne(
                gu => gu.Adress,
                AB =>
                {
                    AB.Property(a => a.Street).HasColumnType("varchar(30)");
                    AB.Property(a => a.City).HasColumnType("varchar(30)");
                }
            );

            builder.Property(gu => gu.CreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
