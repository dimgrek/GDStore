using System;
using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Messages.Events;
using GDStore.Alterations.Messages.Responses;
using GDStore.Alterations.Services.Services;
using MassTransit;

namespace GDStore.Alterations.Handlers
{
    public class AlterationsHandler : IConsumer<AddAlterationRequest>, IConsumer<MakeAlterationCommand>
    {
        private readonly IAlterationService alterationService;

        public AlterationsHandler(IAlterationService alterationService)
        {
            this.alterationService = alterationService;
        }

        public async Task Consume(ConsumeContext<AddAlterationRequest> context)
        {
            var alteration = await alterationService.AddAlteration(context.Message);
            if (alteration != null)
            {
                await context.RespondAsync(new AddAlterationResponse
                {
                    Id = alteration.Id,
                    SuitId = alteration.SuitId,
                    Length = alteration.Length,
                    Name = alteration.Name,
                    SleeveId = alteration.SleeveId,
                    Status = alteration.Status,
                    TrouserLegId = alteration.TrouserLegId
                });

                await context.Publish(new AlterationAddedEvent
                {
                    Id = Guid.NewGuid(),
                    AlterationId = alteration.Id
                });
            }
        }

        public async Task Consume(ConsumeContext<MakeAlterationCommand> context)
        {
            await alterationService.MakeAlteration(context.Message);
            await context.Publish(new AlterationFinishedEvent
            {
                AlterationId = context.Message.AlterationId
            });
        }
    }
}
