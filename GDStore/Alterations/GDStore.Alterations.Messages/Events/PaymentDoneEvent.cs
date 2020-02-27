using System;

namespace GDStore.Alterations.Messages.Events
{
    public class PaymentDoneEvent
    {
        public Guid Id { get; set; }
        public Guid AlterationId { get; set; }
    }
}