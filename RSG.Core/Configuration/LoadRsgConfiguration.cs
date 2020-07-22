using RSG.Core.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSG.Core.Configuration
{
    public class LoadRsgConfiguration : ILoad<RsgConfiguration>
    {
        public RsgConfiguration LoadJson(string file, bool isInternal)
        {
            throw new NotImplementedException();
        }

        public Task<RsgConfiguration> LoadJsonAsync(string file, bool isPath)
        {
            throw new NotImplementedException();
        }
    }
}
