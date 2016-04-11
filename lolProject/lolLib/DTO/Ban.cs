namespace lolLib.DTO
{
    using System;

    public class Ban : IDTO
    {
        public Int16 championId { get; set; }
        public Int16 pickTurn { get; set; }
    }
}