using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;

namespace GDStore.MVC.Services
{
    public interface ISuitService
    {
        Task<List<Suit>> GetAllByCustomerId(Guid customerId);
    }
}