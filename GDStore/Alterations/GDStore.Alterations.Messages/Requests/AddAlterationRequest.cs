using System;
using GDStore.BLL.Interfaces.Enums;

namespace GDStore.Alterations.Messages.Requests
{
    public class AddAlterationRequest
    {
        public Guid SuitId { get; set; }
        public Guid AlterationId { get; set; }
        public string Name { get; set; }
        public Item Item { get; set; }
        public Side Side { get; set; }
        public int Length { get; set; }
    }
}
