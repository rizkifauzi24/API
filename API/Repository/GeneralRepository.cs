﻿using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            myContext.Remove(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            
            try
            {
                return entities.ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Entity Get(Key key)
        {

            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {
            myContext.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
