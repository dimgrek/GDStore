using System;
using MassTransit;

namespace GDStore.WebApi.CommandBus
{
    public class AlterationsesCommandBus : CommandBus, IAlterationsCommandBus
    {
        public AlterationsesCommandBus(IBusControl bus, Uri serviceAddress) : base(bus, serviceAddress)
        {

        }
    }
}