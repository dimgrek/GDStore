using System.Collections.Generic;

namespace GDStore.DAL.Interface.Domain
{
    public class Customer : SQLDataEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Alteration> Alterations { get; set; }
        public ICollection<Suit> Suits { get; set; }
    }
}