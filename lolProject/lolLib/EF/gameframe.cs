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
    
    public partial class gameframe : IEntity
    {
        public gameframe()
        {
            this.frame = new HashSet<frame>();
        }
    
        public long gameId { get; set; }
        public Nullable<int> frameInterval { get; set; }
    
        public virtual game game { get; set; }
        public virtual ICollection<frame> frame { get; set; }
    }
}
