using System;
using System.Collections.Generic;

namespace OnLooker.Core
{
    public class CTagService
    {
        private readonly ITagGateway dbService;
        public CTagService(ITagGateway gateway)
        {
            this.dbService = gateway;
        }
        public Int32 AddTag(CTag entity)
        {
            return dbService.Create(entity);
        }
        public void DeleteTag(Int32 id)
        {
            dbService.Delete(id);
        }
        public CTag GetTagById(Int32 id)
        {
            return dbService.Get(id);
        }

        public Int32 GetTagId(String name)
        {
            return dbService.GetByName(name);
        }
        public List<CTag> GetAllTags()
        {
            return dbService.GetAll();
        }
        public List<string> GetTagsList()
        {
            return dbService.GetAllNames();
        }
        public void UpdateTag(CTag entity)
        {
            dbService.Update(entity);
        }
    }
}
