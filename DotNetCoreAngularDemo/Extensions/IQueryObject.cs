using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Extensions
{
    public interface IQueryObject
    {
        string SortColumn { get; set; }
        bool IsSortAscending { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}
