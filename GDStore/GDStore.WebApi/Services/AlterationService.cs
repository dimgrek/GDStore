using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.WebApi.CommandBus;
using GDStore.WebApi.Models;

namespace GDStore.WebApi.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly IAlterationRepository alterationRepository;
        private readonly IAlterationsCommandBus alterationsCommandBus;

        public AlterationService(IAlterationRepository alterationRepository,
            IAlterationsCommandBus alterationsCommandBus)
        {
            this.alterationRepository = alterationRepository;
            this.alterationsCommandBus = alterationsCommandBus;
        }

        public async Task AddAlteration(AlterationModel model)
        {
            await alterationsCommandBus.SendAsync(new AddAlterationCommand
            {
                CustomerId = model.CustomerId,
                Item = model.Item,
                Length = model.Length,
                Name = model.Name,
                Side = model.Side
            });
        }

        public List<Alteration> GetAllByCustomerId(Guid customerId)
        {
            return alterationRepository.GetAll(x => x.CustomerId == customerId).ToList();
        }

        public List<Alteration> GetAll()
        {
            return alterationRepository.GetAll().ToList();
        }
    }
}