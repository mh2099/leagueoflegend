namespace lolLib.DTO
{
    using System;

    public class Ban : IDTO
    {
        public Int32 championId { get; set; }
        public Int32 pickTurn { get; set; }
    }
}