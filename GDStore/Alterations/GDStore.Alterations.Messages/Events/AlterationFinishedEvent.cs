using System;

namespace GDStore.Alterations.Messages.Events
{
    public class AlterationFinishedEvent
    {
        public Guid Id { get; set; }
        public Guid AlterationId { get; set; }
    }
}