namespace lolLib.Class
{
    using System;
    using System.Collections.Generic;

    public class Participant
    {
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