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
    
    public partial class participantframe
    {
        public participantframe()
        {
            this.participantframes = new HashSet<participantframes>();
            this.participantframes1 = new HashSet<participantframes>();
            this.participantframes2 = new HashSet<participantframes>();
            this.participantframes3 = new HashSet<participantframes>();
            this.participantframes4 = new HashSet<participantframes>();
            this.participantframes5 = new HashSet<participantframes>();
            this.participantframes6 = new HashSet<participantframes>();
            this.participantframes7 = new HashSet<participantframes>();
        }
    
        public int participantId { get; set; }
        public Nullable<int> position_x { get; set; }
        public Nullable<int> position_y { get; set; }
        public Nullable<int> currentGold { get; set; }
        public Nullable<int> totalGold { get; set; }
        public Nullable<int> level { get; set; }
        public Nullable<int> xp { get; set; }
        public Nullable<int> minionsKilled { get; set; }
        public Nullable<int> jungleMinionsKilled { get; set; }
        public Nullable<int> dominionScore { get; set; }
        public Nullable<int> teamScore { get; set; }
    
        public virtual ICollection<participantframes> participantframes { get; set; }
        public virtual ICollection<participantframes> participantframes1 { get; set; }
        public virtual ICollection<participantframes> participantframes2 { get; set; }
        public virtual ICollection<participantframes> participantframes3 { get; set; }
        public virtual ICollection<participantframes> participantframes4 { get; set; }
        public virtual ICollection<participantframes> participantframes5 { get; set; }
        public virtual ICollection<participantframes> participantframes6 { get; set; }
        public virtual ICollection<participantframes> participantframes7 { get; set; }
    }
}
