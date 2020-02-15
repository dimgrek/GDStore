using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.BLL.Interfaces.Models;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.WebApi.Models;

namespace GDStore.WebApi.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISuitRepository suitRepository;

        private readonly IAlterationRepository alterationRepository;
        //private readonly IAlterationsCommandBus alterationsCommandBus;

        public AlterationService(ICustomerRepository customerRepository,
                ISuitRepository suitRepository,
                IAlterationRepository alterationRepository)
            //IAlterationsCommandBus alterationsCommandBus)
        {
            this.customerRepository = customerRepository;
            this.suitRepository = suitRepository;
            this.alterationRepository = alterationRepository;
            //this.alterationsCommandBus = alterationsCommandBus;
        }

        public async Task AddAlteration(AlterationModel model)
        {

            var suit = suitRepository.GetSuitByCustomerId(model.CustomerId);
            var alteration = new Alteration
            {
                Name = model.Name,
                Status = AlterationStatus.Created,
                Length = model.Length,
                CustomerId = model.CustomerId
            };

            if (model.Item == Item.Sleeve)
            {
                var sleeve = suit.Sleeves.FirstOrDefault(x => x.Side == model.Side);
                if (sleeve != null)
                {
                    alteration.SleeveId = sleeve.Id;
                }
            }

            if (model.Item == Item.TrouserLeg)
            {
                var trouserLeg = suit.TrouserLegs.FirstOrDefault(x => x.Side == model.Side);
                if (trouserLeg != null)
                {
                    alteration.TrouserLegId = trouserLeg.Id;
                }
            }

            var customer = await customerRepository.GetByIdAsync(model.CustomerId);

            if (customer.Alterations == null)
            {
                customer.Alterations = new List<Alteration>();
            }

            customer.Alterations.Add(alteration);
            await customerRepository.SaveChangesAsync();
            //await alterationsCommandBus.SendAsync(new AddAlterationCommand
            //{
            //    CustomerId = model.CustomerId,
            //    Item = model.Item,
            //    Length = model.Length,
            //    Name = model.Name,
            //    Side = model.Side
            //});
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