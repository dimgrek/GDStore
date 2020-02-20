using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services.Services;
using MassTransit;

namespace GDStore.Alterations.Handlers
{
    public class AlterationsHandler : IConsumer<AddAlterationCommand>, IConsumer<MakeAlterationCommand>
    {
        private readonly IAlterationService alterationService;

        public AlterationsHandler(IAlterationService alterationService)
        {
            this.alterationService = alterationService;
        }

        public async Task Consume(ConsumeContext<AddAlterationCommand> context)
        {
            await alterationService.AddAlteration(context.Message);
        }

        public async Task Consume(ConsumeContext<MakeAlterationCommand> context)
        {
            await alterationService.MakeAlteration(context.Message);
        }
    }
}
