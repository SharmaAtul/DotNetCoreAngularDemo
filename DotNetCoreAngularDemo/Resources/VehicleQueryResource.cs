using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Resources
{
    public class VehicleQueryResource
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortColumn { get; set; } = "make";
        public bool IsSortAscending { get; set; } = true;
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
