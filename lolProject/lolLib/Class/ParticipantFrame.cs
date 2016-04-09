namespace lolLib.Class
{
    using System;

    public class ParticipantFrame
    {
        public Int32 participantId { get; set; }
        public Position position { get; set; }
        public Int32 currentGold { get; set; }
        public Int32 totalGold { get; set; }
        public Int32 level { get; set; }
        public Int32 xp { get; set; }
        public Int32 minionsKilled { get; set; }
        public Int32 jungleMinionsKilled { get; set; }
        public Int32 dominionScore { get; set; }
        public Int32 teamScore { get; set; }
    }
}