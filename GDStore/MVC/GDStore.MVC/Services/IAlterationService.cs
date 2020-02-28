using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;
using GDStore.MVC.Models;

namespace GDStore.MVC.Services
{
    public interface IAlterationService
    {
        Task<Alteration> AddAlteration(AlterationModel model);
        Task<List<Alteration>> GetAllBySuitId(Guid suitId);
        Task<List<Alteration>> GetAll();
    }
}