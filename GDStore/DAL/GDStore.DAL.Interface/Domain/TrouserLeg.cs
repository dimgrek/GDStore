using System;
using GDStore.BLL.Interfaces.Models;

namespace GDStore.DAL.Interface.Domain
{
    public class TrouserLeg : SQLDataEntity
    {
        public TrouserLeg(int length, Side side)
        {
            Length = length;
            Side = side;
        }
        public int Length { get; set; }
        public Side Side { get; private set; }
        public Guid SuitId { get; set; }
    }
}