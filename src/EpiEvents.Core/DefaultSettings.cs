using System.Configuration;

namespace EpiEvents.Core
{
    public class DefaultSettings : ISettings
    {
        public bool EnableLoadingEvents
        {
            get
            {
                var config = ConfigurationManager.AppSettings[$"EpiEvents:{nameof(EnableLoadingEvents)}"];
                return bool.TryParse(config, out bool enabled) && enabled;
            }
        }
    }
}