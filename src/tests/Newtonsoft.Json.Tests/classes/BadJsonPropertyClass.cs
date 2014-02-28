namespace Newtonsoft.Json.Tests.classes
{
    public class BadJsonPropertyClass
    {
        [JsonProperty("pie")]
        public string Pie = "Yum";

        public string pie = "PieChart!";
    }
}