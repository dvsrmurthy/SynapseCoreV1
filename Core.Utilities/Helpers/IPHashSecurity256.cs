using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{    
    public interface IPHashSecurity256
    {
        string HashPassword(string password, string username);
    }
}
