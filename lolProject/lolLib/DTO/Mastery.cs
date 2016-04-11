namespace lolLib.DTO
{
    using System;

    public class Mastery : IDTO
    {
        public Int32 masteryId { get; set; }
        public SByte rank { get; set; }
    }
}