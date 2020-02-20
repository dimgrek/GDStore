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
        List<Alteration> GetAllByCustomerId(Guid customerId);
        List<Alteration> GetAll();
    }
}