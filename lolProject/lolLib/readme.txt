[lolLib]

_Class
 GameForThisPlayer.cs : classe permettant la concaténation de Game.cs / Player.cs / Team.cs
 SyncProgress.cs : classe de données pour une encapsulation ave IProgress<T>

_DTO
 Classes DTO relatif à la gestion des données issues des parties de League Of Legend

_Engine
 GameAnalyze.cs : analyse de parties et dispatch des parties selon les joueurs
 GameCloudSync.cs : chargement depuis un fichier, mise à jour via le serveur web de League Of Legend et export vers un fichier des parties
 GameDbSync.cs : gestion des parties avec la base de données relationnelle

_ExMethod (Méthodes d'extensions)
 ExBlockingCollection.cs : ajoute la méthode Clear() aux BlockingCollection<T>


(obsolètes)
_RunProcess
 RunProcess.cs : gestion des processus en mode synchro ou asynchrone (utile pour curl.exe)