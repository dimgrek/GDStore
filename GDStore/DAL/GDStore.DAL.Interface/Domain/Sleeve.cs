using System;
using GDStore.BLL.Interfaces.Enums;

namespace GDStore.DAL.Interface.Domain
{
    public class Sleeve : SQLDataEntity
    {
        public int Length { get; set; }
        public Side Side { get;  set; }
    }
}