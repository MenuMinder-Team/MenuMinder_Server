using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class DiningTableDTO
    {
        public Guid CreatedBy { get; set; }
        public string Status { get; set; } = null!;
        public string TableNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
