using System;

namespace GDStore.Alterations.Messages.Commands
{
    public class MakeAlterationCommand
    {
        public Guid AlterationId { get; set; }
        public Guid CustomerId { get; set; }
    }
}