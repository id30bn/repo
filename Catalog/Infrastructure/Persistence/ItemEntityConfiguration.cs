using Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
	public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
			builder.Property(x => x.Name).HasMaxLength(50);
			builder.Property(x => x.Price).HasColumnType("money");

			builder.HasOne(x => x.Category)
				.WithMany()
				.HasForeignKey(x => x.CategoryId)
				.IsRequired();
			builder.OwnsOne(x => x.Description);
			builder.OwnsOne(x => x.Image);
		}
	}
}
