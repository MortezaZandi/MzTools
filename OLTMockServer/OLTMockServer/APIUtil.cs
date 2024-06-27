using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.spag
{
    internal class APIUtil
    {
        public static bool CallApi(string methodName, object methodInput, Method method, string baseUri)
        {
            var restClient = CreateRestClient(methodName, baseUri);

            var restRequest = CreateRestReuest(methodInput, method);

            var response = restClient.ExecuteAsync(restRequest).Result;

            if (!response.IsSuccessful)
            {
                if (response.ErrorException != null)
                {
                    throw response.ErrorException;
                }
                else
                {
                    throw new Exception($"Request to '{baseUri}{methodName}' Failed StatusCode: {response.StatusCode}, StatusDescription: {response.StatusDescription}");
                }
            }

            return (response.IsSuccessful);
        }

        public static TOutput CallApi<TInput, TOutput>(string methodName, TInput methodInput, Method method, string baseUri)
        {
            var restClient = CreateRestClient(methodName, baseUri);

            var restRequest = CreateRestReuest(methodInput, method);

            var response = restClient.ExecuteAsync<TOutput>(restRequest).Result;

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TOutput);
        }

        private static RestClient CreateRestClient(string methodName, string baseUri)
        {
            var wcfFullUri = $"{baseUri}{methodName}";

            var restClient = new RestClient(wcfFullUri)
            {
                Timeout = -1
            };

            return restClient;

        }

        public static RestRequest CreateRestReuest(object methodInput, Method method)
        {
            var request = new RestRequest(method);

            request.AddHeader("Content-Type", "application/json");

            if (methodInput != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter> { new JsonDateTimeConverter() },
                };

                var body = JsonConvert.SerializeObject(methodInput, setting);
                body = body.Replace("\\\\", "\\");
                request.AddParameter("application/json", body, ParameterType.RequestBody);
            }

            return request;
        }

    }

    internal class JsonDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(DateTime) || objectType == typeof(DateTime?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("This is a write only converter.");
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?))
            {
                var dt = DateTime.Parse(value.ToString());

                DateTime epochTime = DateTime.Parse("1970-01-01");

                var milliseconds = dt.Subtract(epochTime).TotalMilliseconds;
                var dateString = "\\/Date(*)\\/";
                dateString = dateString.Replace("*", milliseconds.ToString());
                writer.WriteValue(dateString);
            }
        }
    }
}
