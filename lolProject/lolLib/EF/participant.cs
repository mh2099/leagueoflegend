//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lolLib.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class participant : IEntity
    {
        public participant()
        {
            this.mastery = new HashSet<mastery>();
            this.rune = new HashSet<rune>();
        }
    
        public int participantId { get; set; }
        public long gameId { get; set; }
        public Nullable<short> teamId { get; set; }
        public Nullable<short> championId { get; set; }
        public Nullable<int> spell1Id { get; set; }
        public Nullable<int> spell2Id { get; set; }
        public string highestAchievedSeasonTier { get; set; }
    
        public virtual game game { get; set; }
        public virtual ICollection<mastery> mastery { get; set; }
        public virtual ICollection<rune> rune { get; set; }
        public virtual stat stat { get; set; }
        public virtual timeline timeline { get; set; }
    }
}
