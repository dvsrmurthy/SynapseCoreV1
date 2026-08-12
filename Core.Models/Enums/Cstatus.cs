using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Enums
{
    public enum Cstatus:int
    {
        Approve = 1,
        Pending = 2,
        Rejected = 3,
        TimeExceeded = 4
    }
}
