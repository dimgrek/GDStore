﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;

namespace GDStore.Alterations.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly ISuitRepository suitRepository;
        private readonly ICustomerRepository customerRepository;

        public AlterationService(ISuitRepository suitRepository, ICustomerRepository customerRepository)
        {
            this.suitRepository = suitRepository;
            this.customerRepository = customerRepository;
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
    }
}
