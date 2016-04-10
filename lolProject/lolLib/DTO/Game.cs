namespace lolLib.DTO
{
    using System;
    using System.Collections.Generic;

    public class Game : IDTO
    {
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
        public List<Team> teams { get; set; }
        public List<Participant> participants { get; set; }
        public List<ParticipantIdentity> participantIdentities { get; set; }
        public GameFrame gameFrame { get; set; }
    }
}