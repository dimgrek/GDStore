using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using log4net;

namespace GDStore.MVC.Services
{
    public class SuitService : ISuitService
    {
        private readonly ISuitRepository suitRepository;
        private readonly ILog log = LogManager.GetLogger(typeof(SuitService));


        public SuitService(ISuitRepository suitRepository)
        {
            this.suitRepository = suitRepository;
        }

        public async Task<List<Suit>> GetAllByCustomerId(Guid customerId)
        {
            log.Info($"{nameof(GetAllByCustomerId)} called");

            return await suitRepository.GetAllByCustomerId(customerId);
        }
    }
}