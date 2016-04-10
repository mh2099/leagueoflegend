namespace lolLib.Analyse
{
    using System;
    using System.Collections.Generic;
    using DTO;

    public class GameForThisPlayer : IDTO
    {
        // Global
        public Int64 gameId { get; set; }
        public String platformId { get; set; }
        public Int64 gameCreation { get; set; }
        public Int32 gameDuration { get; set; }
        public Int32 queueId { get; set; }
        public Int32 mapId { get; set; }
        public Int32 seasonId { get; set; }
        public String gameVersion { get; set; }
        public String gameMode { get; set; }
        public String gameType { get; set; }
        // Player
        public Int32 accountId { get; set; }
        public String summonerName { get; set; }
        public Int32 summonerId { get; set; }
        public String currentPlatformId { get; set; }
        public Int32 currentAccountId { get; set; }
        public String matchHistoryUri { get; set; }
        public Int32 profileIcon { get; set; }
        // Team
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
        // Participant
        public Int32 participantId { get; set; }
        public Int32 teamId { get; set; }
        public Int32 championId { get; set; }
        public Int32 spell1Id { get; set; }
        public Int32 spell2Id { get; set; }
        public List<Mastery> masteries { get; set; }
        public List<Rune> runes { get; set; }
        public String highestAchievedSeasonTier { get; set; }
        public Stats stats { get; set; }
        public Timeline timeline { get; set; }
    }
}