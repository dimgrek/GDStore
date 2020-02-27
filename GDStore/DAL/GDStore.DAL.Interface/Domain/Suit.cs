using System;

namespace GDStore.DAL.Interface.Domain
{
    public class Suit : SQLDataEntity
    {
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Sleeve LeftSleeve { get; set; }
        public virtual Sleeve RightSleeve { get; set; }
        public virtual TrouserLeg LeftTrouserLeg { get; set; }
        public virtual TrouserLeg RightTrouserLeg { get; set; }
    }
}