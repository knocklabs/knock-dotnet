namespace KnockTests
{
    using System.Collections.Generic;
    using System.IO;
    using Knock;
    using Newtonsoft.Json;
    using Xunit;

    public class RequestUtilitiesTest
    {
        [Fact]
        public void TestCreateQueryString()
        {
            var options = new FakeOptions
            {
                Id = "some_id",
                Name = "some_name",
            };

            var query = RequestUtilities.CreateQueryString(options);
            Assert.Equal("id=some_id&name=some_name", query);
        }

        [Fact]
        public void TestCreateQueryStringEncodesBooleans()
        {
            var options = new Dictionary<string, object>
            {
                { "create_audience", true },
                { "has_tenant", false },
            };

            var query = RequestUtilities.CreateQueryString(options);
            Assert.Equal("create_audience=true&has_tenant=false", query);
        }

        [Fact]
        public void TestCreateQueryStringEncodesLists()
        {
            var options = new Dictionary<string, object>
            {
                { "tenant_ids", new List<string> { "t1", "t2" } },
            };

            var query = RequestUtilities.CreateQueryString(options);
            Assert.Equal("tenant_ids[]=t1&tenant_ids[]=t2", query);
        }

        [Fact]
        public void TestCreateQueryStringEncodesNestedObjects()
        {
            var options = new Dictionary<string, object>
            {
                {
                    "query_options", new Dictionary<string, object>
                    {
                        { "limit", 10 },
                        { "types", new List<string> { "public_channel" } },
                        { "nested", new Dictionary<string, object> { { "key", "value" } } },
                    }
                },
            };

            var query = RequestUtilities.CreateQueryString(options);
            Assert.Equal("query_options[limit]=10&query_options[types][]=public_channel&query_options[nested][key]=value", query);
        }

        [Fact]
        public void TestSerializeTriggerData()
        {
            var options = new Dictionary<string, object>
            {
                { "trigger_data", new Dictionary<string, object> { { "type", "trex" } } },
            };

            var result = RequestUtilities.SerializeTriggerData(options);
            Assert.Equal("{\"type\":\"trex\"}", result["trigger_data"]);
        }

        [Fact]
        public void TestSerializeTriggerDataLeavesStringsAndNullsAlone()
        {
            var options = new Dictionary<string, object>
            {
                { "trigger_data", "{\"type\":\"trex\"}" },
            };

            Assert.Equal("{\"type\":\"trex\"}", RequestUtilities.SerializeTriggerData(options)["trigger_data"]);
            Assert.Null(RequestUtilities.SerializeTriggerData(null));
        }

        [Fact]
        public void TestCreateHttpContent()
        {
            var options = new FakeOptions
            {
                Id = "some_id",
                Name = "some_name",
            };

            var content = RequestUtilities.CreateHttpContent(
                new KnockRequest
                {
                    Options = options,
                });
            var jsonContent = content.ReadAsStringAsync().Result;
            var dictionaryContent = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonContent);
            var expectedDictionary = new Dictionary<string, string>
            {
                { "id", "some_id" },
                { "name", "some_name" },
            };

            Assert.Equal("application/json", content.Headers.ContentType.ToString());
            Assert.Equal(expectedDictionary, dictionaryContent);
        }

        [Fact]
        public async void TestCreateHttpContentUrlEncoded()
        {
            var options = new FakeOptions
            {
                Id = "some_id",
                Name = "some_name",
            };

            var content = RequestUtilities.CreateHttpContent(
                new KnockRequest
                {
                    IsJsonContentType = false,
                    Options = options,
                });
            var stream = await content.ReadAsStreamAsync();
            var parameters = new StreamReader(stream).ReadToEnd();
            var expectedParameters = "id=some_id&name=some_name";

            Assert.Equal("application/x-www-form-urlencoded", content.Headers.ContentType.MediaType);
            Assert.Equal(expectedParameters, parameters);
        }

        [Fact]
        public void TestToJsonString()
        {
            var options = new FakeOptions
            {
                Id = "some_id",
                Name = "some_name",
            };

            var jsonString = RequestUtilities.ToJsonString(options);
            var dictionaryContent = JsonConvert.DeserializeObject<IDictionary<string, string>>(jsonString);
            var expectedDictionary = new Dictionary<string, string>
            {
                { "id", "some_id" },
                { "name", "some_name" },
            };

            Assert.Equal(expectedDictionary, dictionaryContent);
        }

        [Fact]
        public void TestFromJson()
        {
            var jsonString = "{\"id\": \"some_id\", \"name\": \"some_name\"}";
            var result = RequestUtilities.FromJson<FakeOptions>(jsonString);
            var expectedResult = new FakeOptions
            {
                Id = "some_id",
                Name = "some_name",
            };

            Assert.Equal(expectedResult.Id, result.Id);
            Assert.Equal(expectedResult.Name, result.Name);
        }

        [Fact]
        public void TestParseURLParameters()
        {
            var url = "https://api.knock.app/sso/authorize?domain=foo&state=bar";
            var parsedUrl = RequestUtilities.ParseURLParameters(url);
            var expectedDictionary = new Dictionary<string, string>
            {
                { "domain", "foo" },
                { "state", "bar" },
            };

            Assert.Equal(expectedDictionary, parsedUrl);
        }

        private class FakeOptions : BaseOptions
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
