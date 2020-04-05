namespace MetaHealth.Models {
    using System.Data.Entity;

    public partial class Model : DbContext
    {
        public Model()
        : base("AzureDB")
        //: base("DefaultConnection")
        // : base("HelpAlong")
        {
        }

        public virtual DbSet<SepMood> SepMoods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}