using System;

namespace GDStore.DAL.Interface.Domain
{
    public class Alteration : SQLDataEntity
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public AlterationStatus Status { get; set; }
        public Guid? SleeveId { get; set; }
        public Guid? TrouserLegId { get; set; }
        public int Length { get; set; }
    }
}