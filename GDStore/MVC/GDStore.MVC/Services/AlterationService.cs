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
                SuitId = model.SuitId,
                Item = model.Item,
                Length = model.Length,
                Name = model.Name,
                Side = model.Side
            });
        }

        public async Task<List<Alteration>> GetAllBySuitId(Guid suitId)
        {
            log.Info($"{nameof(GetAllBySuitId)} called");

            return (await alterationRepository.GetAllAsync(x => x.SuitId == suitId)).ToList();
        }

        public async Task<List<Alteration>> GetAll()
        {
            log.Info($"{nameof(GetAll)} called");

            return (await alterationRepository.GetAllAsync()).ToList();
        }
    }
}