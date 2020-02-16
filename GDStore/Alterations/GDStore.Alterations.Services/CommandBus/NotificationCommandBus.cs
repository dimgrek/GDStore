using System;
using MassTransit;

namespace GDStore.Alterations.Services.CommandBus
{
    public class NotificationCommandBus : BLL.Services.Services.CommandBus, INotificationCommandBus
    {
        public NotificationCommandBus(IBusControl bus, Uri serviceAddress) : base(bus, serviceAddress)
        {
        }   
    }
}