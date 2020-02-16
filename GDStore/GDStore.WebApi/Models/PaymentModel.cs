using System;

namespace GDStore.WebApi.Models
{
    public class PaymentModel
    {
        public Guid AlterationId { get; set; }
        public Guid CustomerId { get; set; }
    }
}