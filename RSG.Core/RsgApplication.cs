using RSG.Core.Interfaces;

namespace RSG.Core
{
    public class RsgApplication : IRsgApplication
    {
        public RsgApplication()
        {
            Initialize();
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
