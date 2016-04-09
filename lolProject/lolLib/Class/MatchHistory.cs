namespace lolLib.Class
{
    using System;
    using System.Collections.Generic;

    public class MatchHistory
    {
        public String platformId { get; set; }
        public Int32 accountId { get; set; }
        public Games games { get; set; }
        public List<Int32> shownQueues { get; set; }
    }
}