namespace lolLib.DTO
{
    using System;
    using Newtonsoft.Json;

    public class ParticipantFrames : IDTO
    {
        [JsonProperty(PropertyName = "1")]
        public ParticipantFrame ParticipantFrame1 { get; set; }
        [JsonProperty(PropertyName = "2")]
        public ParticipantFrame ParticipantFrame2 { get; set; }
        [JsonProperty(PropertyName = "3")]
        public ParticipantFrame ParticipantFrame3 { get; set; }
        [JsonProperty(PropertyName = "4")]
        public ParticipantFrame ParticipantFrame4 { get; set; }
        [JsonProperty(PropertyName = "5")]
        public ParticipantFrame ParticipantFrame5 { get; set; }
        [JsonProperty(PropertyName = "6")]
        public ParticipantFrame ParticipantFrame6 { get; set; }
        [JsonProperty(PropertyName = "7")]
        public ParticipantFrame ParticipantFrame7 { get; set; }
        [JsonProperty(PropertyName = "8")]
        public ParticipantFrame ParticipantFrame8 { get; set; }
        [JsonProperty(PropertyName = "9")]
        public ParticipantFrame ParticipantFrame9 { get; set; }
        [JsonProperty(PropertyName = "10")]
        public ParticipantFrame ParticipantFrame10 { get; set; }
    }
}