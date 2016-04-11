namespace lolLib.DTO
{
    using System;

    public class ParticipantFrame : IDTO
    {
        public Int32 participantId { get; set; }
        public Position position { get; set; }
        public Int32 currentGold { get; set; }
        public Int32 totalGold { get; set; }
        public SByte level { get; set; }
        public Int32 xp { get; set; }
        public SByte minionsKilled { get; set; }
        public SByte jungleMinionsKilled { get; set; }
        public Int32 dominionScore { get; set; }
        public Int32 teamScore { get; set; }
    }
}