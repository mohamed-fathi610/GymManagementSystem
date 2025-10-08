using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(C => C.CategoryName).HasColumnType("nvarchar(20)");

            builder
                .HasMany(c => c.Sessions)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}
