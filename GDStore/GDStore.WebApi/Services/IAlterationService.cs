using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;
using GDStore.WebApi.Models;

namespace GDStore.WebApi.Services
{
    public interface IAlterationService
    {
        Task AddAlteration(AlterationModel model);
        List<Alteration> GetAllByCustomerId(Guid customerId);
        List<Alteration> GetAll();
    }
}