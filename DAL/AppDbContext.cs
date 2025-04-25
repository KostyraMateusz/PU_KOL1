using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Studenci { get; set; }
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<Historia> Historie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbUczelnia;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupa>()
                .HasOne(g => g.Rodzic)
                .WithMany(g => g.Dzieci)
                .HasForeignKey(g => g.RodzicID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Grupa)
                .WithMany(g => g.Studenci)
                .HasForeignKey(s => s.IDGrupy)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Student>()
                .ToTable(tb => tb.HasTrigger("TR_StudentHistoria"));
        }
    }
}
