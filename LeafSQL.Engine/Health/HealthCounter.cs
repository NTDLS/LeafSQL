using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Health
{
    public class HealthCounter
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public HealthCounterType Type { get; set; }
        public String Instance { get; set; }
        public double Value { get; set; }
    }
}
