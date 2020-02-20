using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.MVC.CommandBus;
using GDStore.MVC.Models;
using log4net;

namespace GDStore.MVC.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly IAlterationRepository alterationRepository;
        private readonly IAlterationsCommandBus alterationsCommandBus;
        private readonly ILog log = LogManager.GetLogger(typeof(AlterationService));


        public AlterationService(IAlterationRepository alterationRepository,
            IAlterationsCommandBus alterationsCommandBus)
        {
            this.alterationRepository = alterationRepository;
            this.alterationsCommandBus = alterationsCommandBus;
        }

        public async Task AddAlteration(AlterationModel model)
        {
            log.Info($"{nameof(AddAlteration)} called");

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
            log.Info($"{nameof(GetAllByCustomerId)} called");

            return alterationRepository.GetAll(x => x.CustomerId == customerId).ToList();
        }

        public List<Alteration> GetAll()
        {
            log.Info($"{nameof(GetAll)} called");

            var res = alterationRepository.GetAll().ToList();

            return res;
        }
    }
}