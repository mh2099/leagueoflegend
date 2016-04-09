namespace lolLib.Class
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Int32 teamId { get; set; }
        public String win { get; set; }
        public Boolean firstBlood { get; set; }
        public Boolean firstTower { get; set; }
        public Boolean firstInhibitor { get; set; }
        public Boolean firstBaron { get; set; }
        public Boolean firstDragon { get; set; }
        public Boolean firstRiftHerald { get; set; }
        public Int32 towerKills { get; set; }
        public Int32 inhibitorKills { get; set; }
        public Int32 baronKills { get; set; }
        public Int32 dragonKills { get; set; }
        public Int32 vilemawKills { get; set; }
        public Int32 riftHeraldKills { get; set; }
        public Int32 dominionVictoryScore { get; set; }
        public List<Ban> bans { get; set; }
    }
}