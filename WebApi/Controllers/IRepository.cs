using System.Collections.Generic;

namespace WebApi.Controllers
{
    public interface IRepository
    {
        IEnumerable<Entity> All();
        Entity Get(int id);
        void Create(Entity entity);
        void Save(Entity entity);
        void Delete(Entity entity);
    }
}