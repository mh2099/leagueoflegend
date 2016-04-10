namespace lolLib.DTO
{
    using System;
    using System.Collections.Generic;

    public class GameFrame : IDTO
    {
        public List<Frame> frames { get; set; }
        public Int32 frameInterval { get; set; }
    }
}