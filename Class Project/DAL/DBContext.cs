namespace Class_Project.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<AthleteResult> AthleteResults { get; set; }
        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>()
                .HasMany(e => e.AthleteResults)
                .WithRequired(e => e.Athlete)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coach>()
                .HasMany(e => e.Teams)
                .WithRequired(e => e.Coach)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.AthleteResults)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
                .HasMany(e => e.AthleteResults)
                .WithRequired(e => e.Result)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.Athletes)
                .WithRequired(e => e.Team)
                .WillCascadeOnDelete(false);
        }
    }
}
