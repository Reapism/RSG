using RSG.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSG.Core
{
    public class RsgApplication : IRsgApplication
    {
        public RsgApplication()
        {

        }

        public void Initialize()
        {
            GetSettings();
        }

        private void GetSettings()
        {
            GetConfiguration();
        }

        private void GetConfiguration()
        {

        }
    }
}
