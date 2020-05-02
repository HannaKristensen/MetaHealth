using MetaHealth.Models;
using System.Data.Entity;

namespace MetaHealth.DAL
{
    public partial class Model : DbContext
    {
        public Model()
        //: base("AzureDB")
        : base("DefaultConnection")
        // : base("HelpAlong")
        {
        }

        public virtual DbSet<SepMood> SepMoods { get; set; }
        public virtual DbSet<CustomList> CustomLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}