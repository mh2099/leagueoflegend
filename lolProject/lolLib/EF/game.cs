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
    
    public partial class game
    {
        public game()
        {
            this.game_player = new HashSet<game_player>();
            this.participant = new HashSet<participant>();
            this.participantidentities = new HashSet<participantidentities>();
            this.team = new HashSet<team>();
        }
    
        public long gameId { get; set; }
        public string platformId { get; set; }
        public long gameCreation { get; set; }
        public int gameDuration { get; set; }
        public int queueId { get; set; }
        public int mapId { get; set; }
        public int seasonId { get; set; }
        public string gameVersion { get; set; }
        public string gameMode { get; set; }
        public string gameType { get; set; }
    
        public virtual gameframe gameframe { get; set; }
        public virtual ICollection<game_player> game_player { get; set; }
        public virtual ICollection<participant> participant { get; set; }
        public virtual ICollection<participantidentities> participantidentities { get; set; }
        public virtual ICollection<team> team { get; set; }
    }
}
