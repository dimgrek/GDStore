using System;
using System.Data.Entity;
using System.Linq;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;

namespace GDStore.DAL.SQL.Services
{
    public class SuitRepository : SQLRepository<Suit>, ISuitRepository
    {
        public SuitRepository(GDStoreContext context) : base(context)
        {
            
        }

        public Suit GetSuitByCustomerId(Guid customerId)
        {
            return dbSet
                .Where(x => x.CustomerId == customerId)
                .Include(x => x.Sleeves)
                .Include(x => x.TrouserLegs).ToList()
                .First();
        }
    }
}