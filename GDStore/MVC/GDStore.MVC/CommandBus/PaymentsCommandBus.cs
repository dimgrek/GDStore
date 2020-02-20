using System;
using MassTransit;

namespace GDStore.MVC.CommandBus
{
    public class PaymentsCommandBus : BLL.Services.Services.CommandBus, IPaymentsCommandBus
    {
        public PaymentsCommandBus(IBusControl bus, Uri serviceAddress) : base(bus, serviceAddress)
        {
            
        }
    }
}