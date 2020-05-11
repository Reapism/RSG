using RSG.Core.Interfaces;

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
