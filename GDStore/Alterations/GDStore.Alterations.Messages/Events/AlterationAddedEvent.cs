using System;

namespace GDStore.Alterations.Messages.Events
{
    public class AlterationAddedEvent
    {
        public Guid Id { get; set; }
        
        public string AlterationId { get; set; }
    }
}