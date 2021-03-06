namespace Newtonsoft.Json.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class JsonReaderTest
    {
        [Test]
        public void YahooFinance()
        {
            string input = @"{
""matches"" : [
{""t"":""C"", ""n"":""Citigroup Inc."", ""e"":""NYSE"", ""id"":""662713""}
,{""t"":""CHL"", ""n"":""China Mobile Ltd. (ADR)"", ""e"":""NYSE"", ""id"":""660998""}
,{""t"":""PTR"", ""n"":""PetroChina Company Limited (ADR)"", ""e"":""NYSE"", ""id"":""664536""}
,{""t"":""RIO"", ""n"":""Companhia Vale do Rio Doce (ADR)"", ""e"":""NYSE"", ""id"":""671472""}
,{""t"":""RIOPR"", ""n"":""Companhia Vale do Rio Doce (ADR)"", ""e"":""NYSE"", ""id"":""3512643""}
,{""t"":""CSCO"", ""n"":""Cisco Systems, Inc."", ""e"":""NASDAQ"", ""id"":""99624""}
,{""t"":""CVX"", ""n"":""Chevron Corporation"", ""e"":""NYSE"", ""id"":""667226""}
,{""t"":""TM"", ""n"":""Toyota Motor Corporation (ADR)"", ""e"":""NYSE"", ""id"":""655880""}
,{""t"":""JPM"", ""n"":""JPMorgan Chase \x26 Co."", ""e"":""NYSE"", ""id"":""665639""}
,{""t"":""COP"", ""n"":""ConocoPhillips"", ""e"":""NYSE"", ""id"":""1691168""}
,{""t"":""LFC"", ""n"":""China Life Insurance Company Ltd. (ADR)"", ""e"":""NYSE"", ""id"":""688679""}
,{""t"":""NOK"", ""n"":""Nokia Corporation (ADR)"", ""e"":""NYSE"", ""id"":""657729""}
,{""t"":""KO"", ""n"":""The Coca-Cola Company"", ""e"":""NYSE"", ""id"":""6550""}
,{""t"":""VZ"", ""n"":""Verizon Communications Inc."", ""e"":""NYSE"", ""id"":""664887""}
,{""t"":""AMX"", ""n"":""America Movil S.A.B de C.V. (ADR)"", ""e"":""NYSE"", ""id"":""665834""}],
""all"" : false
}
";

            using (JsonReader jsonReader = new JsonReader(new StringReader(input)))
            {
                while (jsonReader.Read())
                {
                    Console.WriteLine(jsonReader.Value);
                }
            }
        }

        [Test]
        public void ReadingIndented()
        {
            string input = @"{
  CPU: 'Intel',
  Drives: [
    'DVD read/writer',
    ""500 gigabyte hard drive""
  ]
}";

            StringReader sr = new StringReader(input);

            using (JsonReader jsonReader = new JsonReader(sr))
            {
                Assert.AreEqual(jsonReader.TokenType, JsonToken.None);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.StartObject);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);
                Assert.AreEqual(jsonReader.Value, "CPU");

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
                Assert.AreEqual(jsonReader.Value, "Intel");

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);
                Assert.AreEqual(jsonReader.Value, "Drives");

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.StartArray);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
                Assert.AreEqual(jsonReader.Value, "DVD read/writer");
                Assert.AreEqual(jsonReader.QuoteChar, '\'');

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
                Assert.AreEqual(jsonReader.Value, "500 gigabyte hard drive");
                Assert.AreEqual(jsonReader.QuoteChar, '"');

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.EndArray);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.EndObject);
            }
        }

        [Test]
        public void ReadingEscapedStrings()
        {
            string input = "{value:'Purple\\r \\n monkey\\'s:\\tdishwasher'}";

            StringReader sr = new StringReader(input);

            using (JsonReader jsonReader = new JsonReader(sr))
            {
                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.StartObject);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
                Assert.AreEqual(jsonReader.Value, "Purple\r \n monkey's:\tdishwasher");
                Assert.AreEqual(jsonReader.QuoteChar, '\'');

                jsonReader.Read();
                Assert.AreEqual(jsonReader.TokenType, JsonToken.EndObject);
            }
        }

        [Test]
        public void ReadNewlineLastCharacter()
        {
            string input = @"{
  CPU: 'Intel',
  Drives: [
    'DVD read/writer',
    ""500 gigabyte hard drive""
  ]
}" + '\n';

            object o = JavaScriptConvert.DeserializeObject(input);
        }

        [Test]
        public void WriteReadWrite()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jsonWriter = new JsonWriter(sw))
            {
                jsonWriter.WriteStartArray();
                jsonWriter.WriteValue(true);

                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("integer");
                jsonWriter.WriteValue(99);
                jsonWriter.WritePropertyName("string");
                jsonWriter.WriteValue("how now brown cow?");
                jsonWriter.WritePropertyName("array");

                jsonWriter.WriteStartArray();
                for (int i = 0; i < 5; i++)
                {
                    jsonWriter.WriteValue(i);
                }

                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("decimal");
                jsonWriter.WriteValue(990.00990099m);
                jsonWriter.WriteEndObject();

                jsonWriter.WriteValue(5);
                jsonWriter.WriteEndArray();

                jsonWriter.WriteEndObject();

                jsonWriter.WriteValue("This is a string.");
                jsonWriter.WriteNull();
                jsonWriter.WriteNull();
                jsonWriter.WriteEndArray();
            }

            string json = sb.ToString();

            JsonSerializer serializer = new JsonSerializer();

            object jsonObject = serializer.Deserialize(new JsonReader(new StringReader(json)));

            sb = new StringBuilder();
            sw = new StringWriter(sb);

            using (JsonWriter jsonWriter = new JsonWriter(sw))
            {
                serializer.Serialize(sw, jsonObject);
            }

            Assert.AreEqual(json, sb.ToString());
        }
    }
}