namespace lolLib.Class
{
    using System;
    using Newtonsoft.Json;

    public class CsDiffPerMinDeltas
    {
        [JsonProperty(PropertyName = "0-10")]
        public Double FirstQuarter { get; set; }

        [JsonProperty(PropertyName = "10-20")]
        public Double SecondQuarter { get; set; }

        [JsonProperty(PropertyName = "20-30")]
        public Double ThirdQuarter { get; set; }

        [JsonProperty(PropertyName = "30-end")]
        public Double LastQuarter { get; set; }
    }
}