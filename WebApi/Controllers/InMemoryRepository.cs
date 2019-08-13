using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    class InMemoryRepository
        : IRepository
    {
        private readonly ConcurrentDictionary<int, Entity> _entities;

        public InMemoryRepository(ConcurrentDictionary<int, Entity> entities)
        {
            _entities = entities;
        }

        public IEnumerable<Entity> All() => _entities.Values;

        public Entity Get(int id) => _entities.TryGetValue(id, out var entity) ? entity : null;

        public void Create(Entity entity)
        {
            entity.Id = _entities.Count + 1;
            _entities.TryAdd(entity.Id, entity);
        }

        public void Save(Entity entity) => _entities.TryUpdate(entity.Id, entity, new Entity{Id = entity.Id});

        public void Delete(Entity entity) => _entities.TryRemove(entity.Id, out _);
    }
}