using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAngularDemo.Core.Models
{
    public class PhotoSettings
    {
        public long MaxSize { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string extension)
        {
            return AcceptedFileTypes.Any(x => x == extension.ToLower());
        }
    }
}
