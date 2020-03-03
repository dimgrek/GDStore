using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Requests;
using GDStore.BLL.Interfaces.Enums;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using log4net;  

namespace GDStore.Alterations.Services.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly ISuitRepository suitRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IAlterationRepository alterationRepository;
        private static readonly ILog log = LogManager.GetLogger(typeof(AlterationService));

        public AlterationService(ISuitRepository suitRepository, 
            ICustomerRepository customerRepository, 
            IAlterationRepository alterationRepository)
        {
            this.suitRepository = suitRepository;
            this.customerRepository = customerRepository;
            this.alterationRepository = alterationRepository;
        }

        public async Task<Alteration> AddAlteration(AddAlterationRequest request)
        {
            log.Info($"{nameof(AddAlteration)} called");

            var suit = await suitRepository.GetByIdAsync(request.SuitId);
            var alteration = new Alteration
            {
                Id = request.AlterationId,
                Name = request.Name,
                Status = AlterationStatus.Created,
                Length = request.Length,
                SuitId = request.SuitId
            };

            switch (request.Item)
            {
                case Item.Sleeve:
                    switch (request.Side)
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
                    switch (request.Side)
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
            if (customer!= null)
            {
                if (customer.Alterations == null)
                {
                    customer.Alterations = new List<Alteration>();

                }
                customer.Alterations.Add(alteration);
                await customerRepository.SaveChangesAsync();
                return alteration;
            }

            return null;
        }

        public async Task MakeAlteration(MakeAlterationCommand command)
        {
            log.Info($"{nameof(MakeAlteration)} called");

            var alteration = await alterationRepository.GetByIdAsync(command.AlterationId);
            if (alteration != null)
            {
                alteration.Status = AlterationStatus.Finished;
                await alterationRepository.SaveChangesAsync();
            }
        }
    }
}
