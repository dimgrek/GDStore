using System;

namespace GDStore.Payments.Messages.Commands
{
    public class PaymentDoneCommand
    {
        public Guid AlterationId { get; set; }
    }
}
