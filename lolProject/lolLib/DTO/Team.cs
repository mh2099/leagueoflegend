namespace lolLib.DTO
{
    using System;
    using System.Collections.Generic;

    public class Team : IDTO
    {
        public Int32 teamId { get; set; }
        public String win { get; set; }
        public Boolean firstBlood { get; set; }
        public Boolean firstTower { get; set; }
        public Boolean firstInhibitor { get; set; }
        public Boolean firstBaron { get; set; }
        public Boolean firstDragon { get; set; }
        public Boolean firstRiftHerald { get; set; }
        public Int16 towerKills { get; set; }
        public Int16 inhibitorKills { get; set; }
        public Int16 baronKills { get; set; }
        public Int16 dragonKills { get; set; }
        public Int16 vilemawKills { get; set; }
        public Int16 riftHeraldKills { get; set; }
        public Int16 dominionVictoryScore { get; set; }
        public List<Ban> bans { get; set; }
    }
}