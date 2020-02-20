using System;
using System.ComponentModel.DataAnnotations;

namespace GDStore.DAL.Interface.Domain
{
    public class SQLDataEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}