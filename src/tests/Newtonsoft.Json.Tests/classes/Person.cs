namespace Newtonsoft.Json.Tests.classes
{
    using System;

    public class Person
    {
        private Guid _internalId;

        private string _firstName;

        [JsonIgnore]
        public Guid InternalId
        {
            get { return _internalId; }
            set { _internalId = value; }
        }

        [JsonProperty("first_name")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
    }
}