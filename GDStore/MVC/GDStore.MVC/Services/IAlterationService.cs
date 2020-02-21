using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;
using GDStore.MVC.Models;

namespace GDStore.MVC.Services
{
    public interface IAlterationService
    {
        Task AddAlteration(AlterationModel model);
        Task<List<Alteration>> GetAllByCustomerId(Guid customerId);
        Task<List<Alteration>> GetAll();
    }
}