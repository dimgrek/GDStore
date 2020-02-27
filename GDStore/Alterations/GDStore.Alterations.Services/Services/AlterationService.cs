using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services.CommandBus;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.Notifications.Messages.Commands;
using log4net;

namespace GDStore.Alterations.Services.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly ISuitRepository suitRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IAlterationRepository alterationRepository;
        private readonly INotificationCommandBus notificationCommandBus;
        private static readonly ILog log = LogManager.GetLogger(typeof(AlterationService));

        public AlterationService(ISuitRepository suitRepository, 
            ICustomerRepository customerRepository, 
            IAlterationRepository alterationRepository,
            INotificationCommandBus notificationCommandBus)
        {
            this.suitRepository = suitRepository;
            this.customerRepository = customerRepository;
            this.alterationRepository = alterationRepository;
            this.notificationCommandBus = notificationCommandBus;
        }

        public async Task AddAlteration(AddAlterationCommand command)
        {
            log.Info($"{nameof(AddAlteration)} called");

            var suit = await suitRepository.GetByIdAsync(command.SuitId);
            var alteration = new Alteration
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Status = AlterationStatus.Created,
                Length = command.Length,
                SuitId = command.SuitId
            };

            switch (command.Item)
            {
                case Item.Sleeve:
                    switch (command.Side)
                    {
                        case Side.Left:
                            alteration.SleeveId = suit.LeftSleeve.Id;
                            break;
                        case Side.Right:
                            alteration.SleeveId = suit.RightSleeve.Id;
                            break;
                    }
                    break;
                case Item.TrouserLeg:
                    switch (command.Side)
                    {
                        case Side.Left:
                            alteration.TrouserLegId = suit.LeftTrouserLeg.Id;
                            break;
                        case Side.Right:
                            alteration.TrouserLegId = suit.RightTrouserLeg.Id;
                            break;
                    }
                    break;
            }

            var customer = await customerRepository.GetByIdAsync(suit.CustomerId);

            if (customer.Alterations == null)
            {
                customer.Alterations = new List<Alteration>();
            }

            customer.Alterations.Add(alteration);
            await customerRepository.SaveChangesAsync();
        }

        public async Task MakeAlteration(MakeAlterationCommand command)
        {
            log.Info($"{nameof(MakeAlterationCommand)} called");

            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            if (alteration != null)
            {
                alteration.Status = AlterationStatus.Finished;
                await alterationRepository.SaveChangesAsync();
                var customer = await customerRepository.GetByIdAsync(command.CustomerId);
                await notificationCommandBus.SendAsync(new SendEmailCommand { Email = customer.Email });
            }
        }
    }
}
