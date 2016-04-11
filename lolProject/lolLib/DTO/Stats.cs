namespace lolLib.DTO
{
    using System;

    public class Stats : IDTO
    {
        public Int32 participantId { get; set; }
        public Boolean win { get; set; }
        public Int16 item0 { get; set; }
        public Int16 item1 { get; set; }
        public Int16 item2 { get; set; }
        public Int16 item3 { get; set; }
        public Int16 item4 { get; set; }
        public Int16 item5 { get; set; }
        public Int16 item6 { get; set; }
        public Int16 kills { get; set; }
        public Int16 deaths { get; set; }
        public Int16 assists { get; set; }
        public Int16 largestKillingSpree { get; set; }
        public Int16 largestMultiKill { get; set; }
        public Int16 killingSprees { get; set; }
        public Int16 longestTimeSpentLiving { get; set; }
        public Int16 doubleKills { get; set; }
        public Int16 tripleKills { get; set; }
        public Int16 quadraKills { get; set; }
        public Int16 pentaKills { get; set; }
        public Int16 unrealKills { get; set; }
        public Int32 totalDamageDealt { get; set; }
        public Int32 magicDamageDealt { get; set; }
        public Int32 physicalDamageDealt { get; set; }
        public Int32 trueDamageDealt { get; set; }
        public Int32 largestCriticalStrike { get; set; }
        public Int32 totalDamageDealtToChampions { get; set; }
        public Int32 magicDamageDealtToChampions { get; set; }
        public Int32 physicalDamageDealtToChampions { get; set; }
        public Int32 trueDamageDealtToChampions { get; set; }
        public Int32 totalHeal { get; set; }
        public Int32 totalUnitsHealed { get; set; }
        public Int32 totalDamageTaken { get; set; }
        public Int32 magicalDamageTaken { get; set; }
        public Int32 physicalDamageTaken { get; set; }
        public Int32 trueDamageTaken { get; set; }
        public Int32 goldEarned { get; set; }
        public Int32 goldSpent { get; set; }
        public Int16 turretKills { get; set; }
        public Int16 inhibitorKills { get; set; }
        public Int16 totalMinionsKilled { get; set; }
        public Int16 neutralMinionsKilled { get; set; }
        public Int16 neutralMinionsKilledTeamJungle { get; set; }
        public Int16 neutralMinionsKilledEnemyJungle { get; set; }
        public Int16 totalTimeCrowdControlDealt { get; set; }
        public Int16 champLevel { get; set; }
        public Int16 visionWardsBoughtInGame { get; set; }
        public Int16 sightWardsBoughtInGame { get; set; }
        public Int16 wardsPlaced { get; set; }
        public Int16 wardsKilled { get; set; }
        public Boolean firstBloodKill { get; set; }
        public Boolean firstBloodAssist { get; set; }
        public Boolean firstTowerKill { get; set; }
        public Boolean firstTowerAssist { get; set; }
        public Boolean firstInhibitorKill { get; set; }
        public Boolean firstInhibitorAssist { get; set; }
        public Int32 combatPlayerScore { get; set; }
        public Int32 objectivePlayerScore { get; set; }
        public Int32 totalPlayerScore { get; set; }
        public Int32 totalScoreRank { get; set; }
    }
}