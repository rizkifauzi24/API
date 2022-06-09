using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profilling>(b => b.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(a => a.Profilling)
                .WithOne(b => b.Education);

            modelBuilder.Entity<University>()
                .HasMany(a => a.Education)
                .WithOne(b => b.University);
            modelBuilder.Entity<AccountRole>()
                .HasKey(bc => new { bc.AccountId, bc.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(bc => bc.AccountId);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(bc => bc.RoleId);

        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Profilling> Profillings { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<AccountRole> AccountRole { get; set; }


    }
}
