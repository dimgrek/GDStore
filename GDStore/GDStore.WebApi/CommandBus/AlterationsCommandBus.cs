using System;
using MassTransit;

namespace GDStore.WebApi.CommandBus
{
    public class AlterationsCommandBus : BLL.Services.Services.CommandBus, IAlterationsCommandBus
    {
        public AlterationsCommandBus(IBusControl bus, Uri serviceAddress) : base(bus, serviceAddress)
        {
        }
    }
}