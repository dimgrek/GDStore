using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Requests;
using GDStore.Alterations.Messages.Responses;
using GDStore.DAL.Interface.Domain;
using GDStore.DAL.Interface.Services;
using GDStore.MVC.Models;
using log4net;
using MassTransit;

namespace GDStore.MVC.Services
{
    public class AlterationService : IAlterationService
    {
        private readonly IAlterationRepository alterationRepository;
        private readonly IRequestClient<AddAlterationRequest, AddAlterationResponse> alterationClient;
        private readonly ILog log;

        public AlterationService(IAlterationRepository alterationRepository,
            IRequestClient<AddAlterationRequest, AddAlterationResponse> alterationClient, 
            ILog log)
        {
            this.alterationRepository = alterationRepository;
            this.alterationClient = alterationClient;
            this.log = log;
        }

        public async Task<Alteration> AddAlteration(AlterationModel model)
        {
            log.Info($"{nameof(AddAlteration)} called");

           var response =  await alterationClient.Request(new AddAlterationRequest
            {
                SuitId = model.SuitId,
                Length = model.Length,
                Item = model.Item,
                Name = model.Name,
                Side = model.Side,
                AlterationId = Guid.NewGuid()
            });

            if (response != null)
            {
                return new Alteration
                {
                    Id = response.Id,
                    SuitId = response.SuitId,
                    Length = response.Length,
                    Name = response.Name,
                    SleeveId = response.SleeveId,
                    Status = response.Status,
                    TrouserLegId = response.TrouserLegId
                };
            }

            return null;
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