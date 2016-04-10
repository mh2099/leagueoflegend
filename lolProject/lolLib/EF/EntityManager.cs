namespace lolLib.EF
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using DTO;

    public static class EntityManager
    {
        /*
        #region Clear
        public static void ClearAll() {}
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
        */
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