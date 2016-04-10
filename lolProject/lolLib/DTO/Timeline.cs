namespace lolLib.DTO
{
    using System;

    public class Timeline : IDTO
    {
        public Int32 participantId { get; set; }
        public CreepsPerMinDeltas creepsPerMinDeltas { get; set; }
        public XpPerMinDeltas xpPerMinDeltas { get; set; }
        public GoldPerMinDeltas goldPerMinDeltas { get; set; }
        public DamageTakenPerMinDeltas damageTakenPerMinDeltas { get; set; }
        public String role { get; set; }
        public String lane { get; set; }
        public CsDiffPerMinDeltas csDiffPerMinDeltas { get; set; }
        public XpDiffPerMinDeltas xpDiffPerMinDeltas { get; set; }
        public DamageTakenDiffPerMinDeltas damageTakenDiffPerMinDeltas { get; set; }
    }
}