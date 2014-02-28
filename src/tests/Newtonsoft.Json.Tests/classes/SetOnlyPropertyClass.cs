namespace Newtonsoft.Json.Tests.classes
{
    public class SetOnlyPropertyClass
    {
        public string Field = "Field";

        public string SetOnlyProperty
        {
            set { }
        }
    }
}
