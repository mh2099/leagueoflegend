namespace lolLib.DTO
{
    using System;
    using System.Collections.Generic;

    public class Games : IDTO
    {
        public Int32 gameIndexBegin { get; set; }
        public Int32 gameIndexEnd { get; set; }
        public Int32 gameTimestampBegin { get; set; }
        public Int32 gameTimestampEnd { get; set; }
        public Int32 gameCount { get; set; }
        public List<Game> games { get; set; }
    }
}