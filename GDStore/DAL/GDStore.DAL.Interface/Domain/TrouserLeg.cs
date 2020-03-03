using GDStore.BLL.Interfaces.Enums;

namespace GDStore.DAL.Interface.Domain
{
    public class TrouserLeg : SQLDataEntity
    {
        public int Length { get; set; }
        public Side Side { get; set; }
    }
}