using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earless.WebApi
{
    public class InitializationOptions
    {
        public InitializationOptions()
        {
            // Set default values.
            ClearTables = false;
            SeedDatabase = false;
        }

        public bool ClearTables { get; set; }
        public bool SeedDatabase { get; set; }
    }
}
