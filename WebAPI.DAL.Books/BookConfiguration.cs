using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.DAL.Model;
using WebAPI.Model;

namespace WebAPI.DAL.Books
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(l => l.Title)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(l => l.Subtitle)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.Resume)
                .HasColumnType("nvarchar(500)");

            builder
                .Property(l => l.Author)
                .HasColumnType("nvarchar(75)");

            builder
                .Property(l => l.CoverImage);

            builder
                .Property(l => l.List)
                .HasColumnType("nvarchar(10)")
                .IsRequired()
                .HasConversion<string>(
                    type => type.ToString(),
                    text => text.ToType()
                );
        }
    }
}
