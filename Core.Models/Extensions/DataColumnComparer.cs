using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core.Models.Extensions
{
    public class DataColumnComparer : IEqualityComparer<DataColumn>
    {
        public bool Equals(DataColumn x, DataColumn y) { return x.Caption == y.Caption; }

        public int GetHashCode(DataColumn obj) { return obj.Caption.GetHashCode(); }

    } 
}
