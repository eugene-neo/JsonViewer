namespace Newtonsoft.Json.Tests.classes
{
    public class JsonPropertyClass
    {
        [JsonProperty("pie")]
        public string Pie = "Yum";

        [JsonIgnore]
        public string pie = "No pie for you!";

        public string pie1 = "PieChart!";

        private int _sweetCakesCount;

        [JsonProperty("sweet_cakes_count")]
        public int SweetCakesCount
        {
            get { return _sweetCakesCount; }
            set { _sweetCakesCount = value; }
        }
    }
}
