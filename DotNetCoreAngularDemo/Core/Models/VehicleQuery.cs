using DotNetCoreAngularDemo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Core.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortAscending { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
