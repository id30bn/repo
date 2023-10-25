using Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
	public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(50);
			builder.OwnsOne(x => x.Image);
			builder.HasOne(x => x.Parent);
				//.WithOne()
				//.HasForeignKey<Category>(x => x.ParentId)
				//.OnDelete(DeleteBehavior.Restrict)
				//.IsRequired(false);
		}
    }
}
