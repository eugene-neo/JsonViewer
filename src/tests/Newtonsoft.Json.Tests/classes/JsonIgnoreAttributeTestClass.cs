namespace Newtonsoft.Json.Tests.classes
{
    public class JsonIgnoreAttributeTestClass
    {
        private int _property = 21;

        private int _ignoredProperty = 12;

        public int Field;

        public int Property
        {
            get { return _property; }
        }

        [JsonIgnore]
        public int IgnoredField;

        [JsonIgnore]
        public int IgnoredProperty
        {
            get { return _ignoredProperty; }
        }
    }
}
