using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mc2.CrudTest.Shared.Serializations
{
    public class NewtonSoftSerializer : IJsonSerializer
    {
        public TOutput Deserialize<TOutput>(string input) =>
            string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject<TOutput>(input);

        public object Deserialize(string input, Type type) =>
            string.IsNullOrWhiteSpace(input) ? null : JsonConvert.DeserializeObject(input, type);

        public string Serialize<TInput>(TInput input) => input == null
            ? string.Empty
            : JsonConvert.SerializeObject(input,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }
}