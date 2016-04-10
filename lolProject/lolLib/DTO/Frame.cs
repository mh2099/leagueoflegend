namespace lolLib.DTO
{
    using System;
    using System.Collections.Generic;

    public class Frame : IDTO
    {
        public ParticipantFrames participantFrames { get; set; }
        public List<Object> events { get; set; }
        public Int32 timestamp { get; set; }
    }
}