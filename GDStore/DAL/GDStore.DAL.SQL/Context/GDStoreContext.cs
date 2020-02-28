using System.Data.Entity;
using GDStore.DAL.Interface.Domain;

namespace GDStore.DAL.SQL.Context
{
    public class GDStoreContext : DbContext
    {
        public GDStoreContext() : base("GDStoreContext")
        {
            Database.SetInitializer(new GDStoreContextInitializer());
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Alteration> Alterations { get; set; }
        public DbSet<Suit> Suits { get; set; }
        public DbSet<Sleeve> Sleeves { get; set; }
        public DbSet<TrouserLeg> TrouserLegs { get; set; }
        public DbSet<AlterationSaga> AlterationSagas { get; set; }
    }
}