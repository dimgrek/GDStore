using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services;
using GDStore.Alterations.Services.Services;
using log4net;
using MassTransit;

namespace GDStore.Alterations.Handlers
{
    public class AlterationsHandler : IConsumer<AddAlterationCommand>, IConsumer<MakeAlterationCommand>
    {
        private readonly IAlterationService alterationService;
        private static readonly ILog log = LogManager.GetLogger(typeof(AlterationsHandler));

        public AlterationsHandler(IAlterationService alterationService)
        {
            this.alterationService = alterationService;
        }

        public async Task Consume(ConsumeContext<AddAlterationCommand> context)
        {
            log.Info($"{nameof(AddAlterationCommand)} handler called");

            await alterationService.AddAlteration(context.Message);
        }

        public async Task Consume(ConsumeContext<MakeAlterationCommand> context)
        {
            log.Info($"{nameof(MakeAlterationCommand)} handler called");

            await alterationService.MakeAlteration(context.Message);
        }
    }
}
