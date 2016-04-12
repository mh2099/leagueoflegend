namespace lolLib.EF
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using DTO;

    public static class EntityManager
    {
        #region Manage
        public static void CreateDataBase()
        {
            //Database.SetInitializer<lolEntities>(new DropCreateDatabaseIfModelChanges<lolEntities>());

            using (var nE = new lolEntities())
            {
                nE.Database.Delete();
                nE.Database.Create();
                //nE.Database.CreateIfNotExists();
                //nE.Database.Initialize(force: true);
            }
        }
        public static void ClearAll()
        {
            using (var nE = new lolEntities())
            {
                var sqlList = new List<String>()
                {
                    "SET foreign_key_checks = 0",
                    "DELETE FROM Ban",
                    "DELETE FROM Frame",
                    "DELETE FROM Game",
                    "DELETE FROM Gameframe",
                    "DELETE FROM Mastery",
                    "DELETE FROM Participant",
                    "DELETE FROM ParticipantFrame",
                    "DELETE FROM ParticipantFrames",
                    "DELETE FROM Player",
                    "DELETE FROM Rune",
                    "DELETE FROM Stat",
                    "DELETE FROM Team",
                    "DELETE FROM Timeline",
                    "SET foreign_key_checks = 1",
                };

                foreach (var sql in sqlList)
                    nE.Database.ExecuteSqlCommand(sql);
            }
        }
        #endregion
        #region Ping
        public static Boolean Ping()
        {
            using (var nE = new lolEntities())
            {
                try
                {
                    var count = nE.game.Count();
                    return true;
                }
                catch {
                    return false;
                }
            }
        }
        #endregion
        #region Game
        public static Boolean GameExist(Int64 GameId)
        {
            using (var nE = new lolEntities())
            {
                var count = nE.game.Any(a => a.gameId == GameId);
                return count;
            }
        }
        public static void GameDelete(Int64 GameId)
        {
            using (var nE = new lolEntities())
            {
                var game = nE.game.FirstOrDefault(a => a.gameId == GameId);
                if (game != null)
                    nE.game.Remove(game);
            }
        }
        public static void GameAdd(Game Game)
        {
            using (var nE = new lolEntities())
            {
                // Game
                var entity = new game();
                MapTo.MappingToEntity(Game, ref entity);
                var game = nE.game.Add(entity);
                //
                nE.SaveChanges();
            }
        }
        #endregion
    }

    public static class EntityManager<TEntity, TDTO>
            where TEntity : IEntity, new()
            where TDTO : IDTO, new()
    {
        /*
        #region Get
        public static TDTO Select(Int32 id)
        {
            using (var nE = new lolEntities())
            {
                var entity = nE.Set<TEntity>().FirstOrDefault(a => a.id == id);
                return entity == null ? null : MapTo.MappingToDTO<TEntity, TDTO>(entity) as TDTO;
            }
        }
        public static IEnumerable<TDTO> Load()
        {
            using (var nE = new lolEntities())
            {
                var entityList = nE.Set<TEntity>().OrderBy(a => a.id).ToList();
                return entityList.Select(entity => MapTo.MappingToDTO<TEntity, TDTO>(entity) as TDTO);
            }
        }
        #endregion
        #region Set
        /// <summary>
        /// Save DTO to DataBase, return DTO.Order (Entity.id) or 0 (Error)
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns></returns>
        public static Int32 Save(TDTO Dto)
        {
            using (var nE = new lolEntities())
            {
                var entity = nE.Set<TEntity>().FirstOrDefault(a => a.id == Dto.Order);
                var mapType = Dto.GetType();

                // New flag (useful for ControllerDTO)
                var isNew = false;

                // New entity
                if (entity == null)
                {
                    isNew = true;
                    entity = new TEntity();

                    if (Dto.Order != 0)
                        entity.id = Dto.Order;

                    nE.Set<TEntity>().Add(entity);
                }

                // Convert DTO to entity
                MapTo.MappingToEntity(Dto, ref entity, isNew);
                nE.SaveChanges();

                return entity.id;
            }
        }
        public static Int32[] SaveAll(IEnumerable<TDTO> DtoList)
        {
            if (DtoList == null) return new Int32[] { 0 };
            if (!DtoList.Any()) return new Int32[] { 0 };

            var rArray = new Int32[DtoList.Count()];

            var i = 0;
            foreach (var dto in DtoList)
            {
                var r = Save(dto);

                rArray[i] = r;
            }

            return rArray;
        }
        #endregion
        #region Clear
        public static Boolean Delete(Int32 Id)
        {
            using (var nE = new lolEntities())
            {
                var entity = nE.Set<TEntity>().FirstOrDefault(a => a.id == Id);
                if (entity == null) return false;

                nE.Set<TEntity>().Remove(entity);
                nE.SaveChanges();

                var count = nE.Set<TEntity>().Count(a => a.id == Id);
                return count == 0;
            }
        }
        public static Boolean Clear()
        {
            using (var nE = new lolEntities())
            {
                var sql = $"DELETE FROM {typeof(TEntity).Name}";
                nE.Database.ExecuteSqlCommand(sql);

                var count = nE.Set<TEntity>().Count();
                return count == 0;
            }
        }
        #endregion
        */
    }
}