using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enum
{
    public enum EnumFoodStatus
    {
        [Description("PENDING")]
        PENDING,
        [Description("HIDDEN")]
        HIDDEN,
        [Description("AVAILABLE")]
        AVAILABLE,
        DELETED
    }
}
