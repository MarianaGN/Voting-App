using System;
using AndriiCoursework.Models;
using AndriiCoursework.Models.EntityModels;
using AndriiCoursework.Models.Static;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AndriiCoursework.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<Elector, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Elector> Electors { get; set; }

        public DbSet<BallotForm> BallotForms { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Elector>()
                .HasOne(e => e.BallotForm)
                .WithOne(b => b.Elector)
                .HasForeignKey<BallotForm>(f => f.ElectorId);
        }
    }
}
