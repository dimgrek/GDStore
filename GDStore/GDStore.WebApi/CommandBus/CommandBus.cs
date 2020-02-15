using System;
using System.Threading.Tasks;
using MassTransit;

namespace GDStore.WebApi.CommandBus
{
    public class CommandBus : ICommandBus
    {
        private readonly IBusControl bus;
        private readonly Uri serviceAddress;

        protected CommandBus(IBusControl bus, Uri serviceAddress)
        {
            this.bus = bus;
            this.serviceAddress = serviceAddress;
        }

        public async Task SendAsync<T>(T command) where T : class
        {
            var endpoint = await bus.GetSendEndpoint(serviceAddress);

            await endpoint.Send<T>(command);
        }
    }
}