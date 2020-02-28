using System;
using GDStore.DAL.Interface.Domain;

namespace GDStore.Alterations.Messages.Responses
{
    public class AddAlterationResponse
    {
        public Guid Id { get; set; }
        public Guid SuitId { get; set; }
        public string Name { get; set; }
        public AlterationStatus Status { get; set; }
        public Guid? SleeveId { get; set; }
        public Guid? TrouserLegId { get; set; }
        public int Length { get; set; }
    }
}