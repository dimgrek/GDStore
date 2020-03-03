using System;

namespace GDStore.Alterations.Messages.Events
{
    public class AlterationFinishedEvent
    {
        public Guid AlterationId { get; set; }
    }
}