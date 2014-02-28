namespace Newtonsoft.Json.Tests.classes
{
    public class GetOnlyPropertyClass
    {
        public string Field = "Field";

        public string GetOnlyProperty
        {
            get { return "GetOnlyProperty"; }
        }
    }
}
