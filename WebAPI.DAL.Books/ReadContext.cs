using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.DAL.Model;

namespace WebAPI.DAL.Books
{
    public class ReadContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public ReadContext(DbContextOptions<ReadContext> options)
            : base(options)
        {
            //Create database and table struct
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Book>(new BookConfiguration());
        }
    }
}
