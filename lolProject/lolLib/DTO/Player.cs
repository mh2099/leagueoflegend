namespace lolLib.DTO
{
    using System;

    public class Player : IDTO
    {
        public String platformId { get; set; }
        public Int32 accountId { get; set; }
        public String summonerName { get; set; }
        public Int32 summonerId { get; set; }
        public String currentPlatformId { get; set; }
        public Int32 currentAccountId { get; set; }
        public String matchHistoryUri { get; set; }
        public Int32 profileIcon { get; set; }
    }
}