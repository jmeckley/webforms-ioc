using Dapper;
using WebApplication.Core.Database;

namespace WebApplication.Core.Mvp.MyEntity
{
    public class DapperMyEntityRepository
        : ICrudRepository<Model.MyEntity>
    {
        private readonly DatabaseConnectionContext _database;

        public DapperMyEntityRepository(DatabaseConnectionContext database)
        {
            _database = database;
        }

        public Model.MyEntity Get(int id)
        {
            return _database.Connection.QueryFirstOrDefault<Model.MyEntity>("select * from [Table] where [Id] = @id", new {id}, _database.Transaction);
        }

        public Model.MyEntity Insert(Model.MyEntity item)
        {
            var id = _database.Connection.QueryFirst<int>("insert into [Table] ([Name], [Age]) OUTPUT INSERTED.[Id] values (@Name, @Age)", new { item.Name, item.Age }, _database.Transaction);
            return Get(id);
        }

        public Model.MyEntity Update(Model.MyEntity item)
        {
            _database.Connection.Execute("Update [Table] Set [Name] = @Name, [Age] = @Age where [Id] = @id", new { item.Name, item.Age, item.Id }, _database.Transaction);
            return Get(item.Id);
        }

        public void Delete(Model.MyEntity item)
        {
            _database.Connection.Execute("delete from [Table] where [Id] = @id", new { item.Id }, _database.Transaction);
        }
    }
}