namespace lolLib.Class
{
    using System;
    using System.Collections.Generic;

    public class Frame
    {
        public ParticipantFrames participantFrames { get; set; }
        public List<Object> events { get; set; }
        public Int32 timestamp { get; set; }
    }
}