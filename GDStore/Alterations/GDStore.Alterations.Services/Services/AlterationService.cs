using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services.CommandBus;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.Notifications.Messages.Commands;

namespace GDStore.Alterations.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly ISuitRepository suitRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IAlterationRepository alterationRepository;
        private readonly INotificationCommandBus notificationCommandBus;

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
            var suit = suitRepository.GetSuitByCustomerId(command.CustomerId);
            var alteration = new Alteration
            {
                Name = command.Name,
                Status = AlterationStatus.Created,
                Length = command.Length,
                CustomerId = command.CustomerId
            };

            if (command.Item == Item.Sleeve)
            {
                var sleeve = suit.Sleeves.FirstOrDefault(x => x.Side == command.Side);
                if (sleeve != null)
                {
                    alteration.SleeveId = sleeve.Id;
                }
            }

            if (command.Item == Item.TrouserLeg)
            {
                var trouserLeg = suit.TrouserLegs.FirstOrDefault(x => x.Side == command.Side);
                if (trouserLeg != null)
                {
                    alteration.TrouserLegId = trouserLeg.Id;
                }
            }

            var customer = await customerRepository.GetByIdAsync(command.CustomerId);

            if (customer.Alterations == null)
            {
                customer.Alterations = new List<Alteration>();
            }

            customer.Alterations.Add(alteration);
            await customerRepository.SaveChangesAsync();
        }

        public async Task MakeAlteration(MakeAlterationCommand command)
        {
            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            if (alteration != null)
            {
                alteration.Status = AlterationStatus.Finished;
                await alterationRepository.SaveChangesAsync();
            }

            var customer = await customerRepository.GetByIdAsync(command.CustomerId);
            await notificationCommandBus.SendAsync(new SendEmailCommand {Email = customer.Email});
        }
    }
}
