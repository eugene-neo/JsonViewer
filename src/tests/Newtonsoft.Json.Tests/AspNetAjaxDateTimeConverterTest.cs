namespace Newtonsoft.Json.Tests
{
    using System;
    using classes;
    using Converters;
    using NUnit.Framework;

    [TestFixture]
    public class AspNetAjaxDateTimeConverterTest
    {
        [Test]
        public void Serialize()
        {
            var c = new DateTimeTestClass();
            c.DateTimeField = new DateTime(2008, 12, 12, 12, 12, 12, 12);
            c.PreField = "Pre";
            c.PostField = "Post";

            string json = JavaScriptConvert.SerializeObject(c, new AspNetAjaxDateTimeConverter());

            Assert.AreEqual(
                @"{""PreField"":""Pre"",""DateTimeField"":""@1229083932012@"",""PostField"":""Post""}",
                json);
        }

        [Test]
        public void DeSerialize()
        {
            var c =
                JavaScriptConvert.DeserializeObject<DateTimeTestClass>(
                    @"{""PreField"":""Pre"",""DateTimeField"":""@1229083932012@"",""PostField"":""Post""}",
                    new AspNetAjaxDateTimeConverter());

            Assert.AreEqual(new DateTime(2008, 12, 12, 12, 12, 12, 12), c.DateTimeField);
            Assert.AreEqual("Pre", c.PreField);
            Assert.AreEqual("Post", c.PostField);
        }
    }
}