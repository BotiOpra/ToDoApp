using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.Utilities
{
    public static class RandomIdGenerator
    {
        public static string GenerateId()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
    }
}
