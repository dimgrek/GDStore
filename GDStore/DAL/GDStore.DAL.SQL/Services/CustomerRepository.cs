using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.DAL.SQL.Context;

namespace GDStore.DAL.SQL.Services
{
    public class CustomerRepository : SQLRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(GDStoreContext context): base(context)
        {
            
        }
    }
}