using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Suit>> GetAllByCustomerId(Guid customerId)
        {
            return (await GetAllAsync(x => x.CustomerId == customerId)).ToList();
        }
    }
}