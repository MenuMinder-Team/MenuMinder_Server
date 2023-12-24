using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enum;

namespace BusinessObjects.DTO.FoodOrderDTO
{
    public record FoodOrderAddDTO
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public int Price { get; set; }
    }
}
