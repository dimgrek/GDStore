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
        private readonly ILog log;

        public SuitService(ISuitRepository suitRepository, ILog log)
        {
            this.suitRepository = suitRepository;
            this.log = log;
        }

        public async Task<List<Suit>> GetAllByCustomerId(Guid customerId)
        {
            log.Info($"{nameof(GetAllByCustomerId)} called");

            return await suitRepository.GetAllByCustomerId(customerId);
        }
    }
}