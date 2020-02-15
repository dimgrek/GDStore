using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GDStore.BLL.Interfaces.Models;

namespace GDStore.WebApi.Models
{
    public class AlterationModel
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public Item Item { get; set; }
        public Side Side { get; set; }
        [Range(-5, 5)]
        public int Length { get; set; }
    }
}