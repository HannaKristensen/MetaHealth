namespace MetaHealth {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HelpAlongDB : DbContext {
        public HelpAlongDB()
            : base("name=HelpAlongDB") {
        }

        public virtual DbSet<APIToDoList> APIToDoLists { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CustomLevel> CustomLevels { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<Mood> Moods { get; set; }
        public virtual DbSet<MoodsInBetween> MoodsInBetweens { get; set; }
        public virtual DbSet<PreLevelList> PreLevelLists { get; set; }
        public virtual DbSet<ToDoList> ToDoLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Lists)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.FK_UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.MoodsInBetweens)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.FK_UserTable)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<List>()
                .HasMany(e => e.APIToDoLists)
                .WithRequired(e => e.List)
                .HasForeignKey(e => e.FK_List)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<List>()
                .HasMany(e => e.CustomLevels)
                .WithRequired(e => e.List)
                .HasForeignKey(e => e.FK_List)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<List>()
                .HasMany(e => e.ToDoLists)
                .WithRequired(e => e.List)
                .HasForeignKey(e => e.FK_List)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MoodsInBetween>()
                .HasMany(e => e.Moods)
                .WithRequired(e => e.MoodsInBetween)
                .HasForeignKey(e => e.FK_MoodsInBetween)
                .WillCascadeOnDelete(false);
        }
    }
}
