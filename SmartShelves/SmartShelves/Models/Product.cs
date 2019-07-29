using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShelves.Models
{
    using Newtonsoft.Json;

    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public string DeviceId { get; set; }

    }
}
