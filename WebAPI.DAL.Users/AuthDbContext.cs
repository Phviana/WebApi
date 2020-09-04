using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.DAL.Users
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            //Create database and table struct
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration<User>(new UserConfiguration());
        }
    }
}
