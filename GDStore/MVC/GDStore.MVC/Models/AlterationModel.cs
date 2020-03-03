using System;
using System.ComponentModel.DataAnnotations;
using GDStore.BLL.Interfaces.Enums;

namespace GDStore.MVC.Models
{
    public class AlterationModel
    {
        public Guid SuitId { get; set; }
        public string Name { get; set; }
        public Item Item { get; set; }
        public Side Side { get; set; }
        [Range(-5, 5)]
        public int Length { get; set; }
    }
}