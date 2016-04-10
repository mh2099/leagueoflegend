namespace lolLib.DTO
{
    using System;

    public class ParticipantIdentity : IDTO
    {
        public Int32 participantId { get; set; }
        public Player player { get; set; }
    }
}