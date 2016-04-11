namespace lolLib.EF
{
    using System;
    using DTO;

    public static class MapTo
    {
        public static void MappingToEntity<TDTO, TEntity>(TDTO dto, ref TEntity entity, Boolean isNew = false)
            where TDTO : IDTO
            where TEntity : IEntity
        {
            var mapType = dto.GetType();
            // Ban DTO
            if (mapType == typeof (Ban))
            {
                var d = dto as Ban;
                var e = entity as ban;
                if (d == null || e == null) return;
                e.championId = d.championId;
                e.pickTurn = d.pickTurn;
                return;
            }
            // Game DTO
            if (mapType == typeof (Game))
            {
                var d = dto as Game;
                var e = entity as game;
                if (d == null || e == null) return;
                e.gameId = d.gameId;
                e.platformId = d.platformId;
                e.gameCreation = d.gameCreation;
                e.gameDuration = d.gameDuration;
                e.queueId = d.queueId;
                e.mapId = d.mapId;
                e.seasonId = d.seasonId;
                e.gameVersion = d.gameVersion;
                e.gameMode = d.gameMode;
                e.gameType = d.gameType;
                // TODO
                return;
            }
            // Mastery DTO
            if (mapType == typeof (Mastery))
            {
                var d = dto as Mastery;
                var e = entity as mastery;
                if (d == null || e == null) return;
                e.masteryId = d.masteryId;
                e.rank = d.rank;
                return;
            }
        }

        public static IDTO MappingToCRDTO<TEntity, TCRDTO>(TEntity entity)
            where TEntity : IEntity
            where TCRDTO : IDTO
        {
            var mapType = entity.GetType();
            // ban
            if (mapType == typeof (ban))
            {
                var e = entity as ban;
                return new Ban
                {
                    championId = e.championId.GetValueOrDefault(),
                    pickTurn = e.pickTurn.GetValueOrDefault()
                };
            }
            // game
            if (mapType == typeof (game))
            {
                var e = entity as game;
                return new Game
                {
                    gameId = e.gameId,
                    platformId = e.platformId,
                    gameCreation = e.gameCreation,
                    gameDuration = e.gameDuration,
                    queueId = e.queueId,
                    mapId = e.mapId,
                    seasonId = e.seasonId,
                    gameVersion = e.gameVersion,
                    gameMode = e.gameMode,
                    gameType = e.gameType,
                    // TODO
                };
            }
            // mastery
            if (mapType == typeof (mastery))
            {
                var e = entity as mastery;
                return new Mastery
                {
                    masteryId = e.masteryId,
                    rank = e.rank.GetValueOrDefault()
                };
            }
            //
            return null;
        }
    }
}