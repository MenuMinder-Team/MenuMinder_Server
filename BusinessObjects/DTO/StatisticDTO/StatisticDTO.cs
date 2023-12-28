using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.StatisticDTO
{
    public record ResultRevenueDTO
    {
        DateOnly reportDate { get; set; }
        long total_revenue { get; set; }
    }
}
