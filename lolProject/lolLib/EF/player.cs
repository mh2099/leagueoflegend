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
    
    public partial class player : IEntity
    {
        public player()
        {
            this.game_player = new HashSet<game_player>();
            this.participantidentities = new HashSet<participantidentities>();
        }
    
        public int accountId { get; set; }
        public string summonerName { get; set; }
        public int summonerId { get; set; }
        public string currentPlatformId { get; set; }
        public int currentAccountId { get; set; }
        public string matchHistoryUri { get; set; }
        public int profileIcon { get; set; }
    
        public virtual ICollection<game_player> game_player { get; set; }
        public virtual ICollection<participantidentities> participantidentities { get; set; }
    }
}
