using System;
using GDStore.BLL.Interfaces.Models;

namespace GDStore.DAL.Interface.Domain
{
    public class Sleeve : SQLDataEntity
    {

        public int Length { get; set; }
        public Side Side { get;  set; }
        public Guid SuitId { get; set; }
    }
}