using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AzureTwitter.CommunicationUtils
{
    public class DataSerializer : IDataSerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public DataSerializer()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, _serializerSettings);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _serializerSettings);
        }
    }
}
