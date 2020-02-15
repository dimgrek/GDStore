using System;
using System.Collections.Generic;

namespace GDStore.DAL.Interface.Domain
{
    public class Suit : SQLDataEntity
    {
        public string Name { get; set; }
        public Guid CustomerId { get; set; }
        public virtual ICollection<Sleeve> Sleeves { get; set; }
        public virtual ICollection<TrouserLeg> TrouserLegs { get; set; }
    }
}