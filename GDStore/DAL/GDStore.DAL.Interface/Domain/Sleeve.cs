using System;
using GDStore.BLL.Interfaces.Models;

namespace GDStore.DAL.Interface.Domain
{
    public class Sleeve : SQLDataEntity
    {
        public Sleeve(int length, Side side)
        {
            Length = length;
            Side = side;
        }

        public int Length { get; set; }
        public Side Side { get; private set; }
        public Guid SuitId { get; set; }
    }
}