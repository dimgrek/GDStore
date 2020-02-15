using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;

namespace GDStore.DAL.SQL.Services
{
    public class AlterationRepository : SQLRepository<Alteration>, IAlterationRepository
    {
        public AlterationRepository(GDStoreContext context) : base(context)
        {
            
        }
    }
}