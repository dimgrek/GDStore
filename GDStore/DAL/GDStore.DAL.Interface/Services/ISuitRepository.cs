using System;
using GDStore.DAL.Interface.Domain;

namespace GDStore.DAL.Interface.Services
{
    public interface ISuitRepository : ISQLRepository<Suit>
    {
        Suit GetSuitByCustomerId(Guid customerId);
    }
}