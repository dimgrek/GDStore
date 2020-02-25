using System;
using GDStore.BLL.Interfaces.Models;

namespace GDStore.Alterations.Messages.Commands
{
    public class AddAlterationCommand
    {
        public Guid SuitId { get; set; }
        public string Name { get; set; }
        public Item Item { get; set; }
        public Side Side { get; set; }
        public int Length { get; set; }
    }
}
