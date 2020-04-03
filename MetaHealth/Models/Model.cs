namespace MetaHealth.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using System.Data.Entity;

    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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