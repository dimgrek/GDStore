using GDStore.BLL.Interfaces.Models;

namespace GDStore.DAL.Interface.Domain
{
    public class TrouserLeg : SQLDataEntity
    {
        public int Length { get; set; }
        public Side Side { get; set; }
    }
}