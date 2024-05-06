using LabAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LabAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Book_Author> Books_Author { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book_Author>()
                .HasKey(bc => new { bc.BooksId, bc.AuthorsId });

            builder.Entity<Book_Author>()
                .HasOne(bc => bc.Books)
                .WithMany(b => b.Book_Authors)
                .HasForeignKey(bc => bc.BooksId);

            builder.Entity<Book_Author>()
                .HasOne(bc => bc.Authors)
                .WithMany(a => a.Book_Authors)
                .HasForeignKey(bc => bc.AuthorsId);

            new DbInitializer(builder).Seed();
        }
        public class DbInitializer
        {
            private readonly ModelBuilder _builder;
            public DbInitializer(ModelBuilder builder)
            {
                this._builder = builder;
            }
            public void Seed()
            {
                _builder.Entity<Books>().HasData(
                    new Books
                    {
                        BooksId = 1,
                        Title = "Book 1",
                        Description = "Doraemon",
                        PublishersId = 1,
                        Genre = "Nam"
                    },
                    new Books
                    {
                        BooksId = 2,
                        Title = "Book 2",
                        Description = "Cây Khế",
                        PublishersId = 2,
                        Genre = "Nam"
                    }
                );

                _builder.Entity<Authors>().HasData(
                    new Authors
                    {
                        AuthorsId = 1,
                        FullName = "Fujiko Fujio",
                    },
                    new Authors
                    {
                        AuthorsId = 2,
                        FullName = "Trần Cao Duyên ",
                    }
                );
                _builder.Entity<Publishers>().HasData(
                    new Publishers
                    {
                        PublishersId = 1,
                        Name = "Nhà Xuất Bản Kim Đồng",
                    },
                    new Publishers
                    {
                        PublishersId = 2,
                        Name = "Bộ Giáo Dục",
                    }
                );
                _builder.Entity<Book_Author>().HasData(
                   new Book_Author
                   {
                       Book_AuthorId = 1,
                       BooksId = 1,
                       AuthorsId = 1
                   },
                   new Book_Author
                   {
                       Book_AuthorId = 2,
                       BooksId = 2,
                       AuthorsId = 2
                   }
                   );
            }
        }
    }
}
