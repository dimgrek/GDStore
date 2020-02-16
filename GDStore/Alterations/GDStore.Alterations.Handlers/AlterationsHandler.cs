﻿using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using GDStore.Alterations.Services;
using MassTransit;

namespace GDStore.Alterations.Handlers
{
    public class AlterationsHandler : IConsumer<AddAlterationCommand>
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
    }
}
