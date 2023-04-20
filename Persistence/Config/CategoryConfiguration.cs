using Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder
            //    .HasData(
            //    new Category("Root")
            //    {
            //        // Первоначальне значение первичного ключа не может быть null
            //        Id = 1
            //    }
            //    );


            builder
                .HasOne(c => c.Parent)
                .WithMany(c => c.Categories)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Property(c => c.Sort)
            //    .ValueGeneratedOnAdd();


        }
    }
}
