namespace Opensource.Json.Viewer
{
    using System.Configuration;

    public class ViewerConfiguration: ConfigurationSection
    {
        [ConfigurationProperty("plugins")]
        public KeyValueConfigurationCollection Plugins
        {
            get
            {
                return (KeyValueConfigurationCollection)base["plugins"];
            }
        }
    }
}
