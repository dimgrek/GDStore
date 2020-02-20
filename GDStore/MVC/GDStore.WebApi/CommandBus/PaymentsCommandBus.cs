using System;
using MassTransit;

namespace GDStore.WebApi.CommandBus
{
    public class PaymentsCommandBus : BLL.Services.Services.CommandBus, IPaymentsCommandBus
    {
        public PaymentsCommandBus(IBusControl bus, Uri serviceAddress) : base(bus, serviceAddress)
        {
            
        }
    }
}