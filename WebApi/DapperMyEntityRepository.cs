using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace WebApi
{
    public class DapperMyEntityRepository
        : IRepository<MyEntity>
    {
        private readonly DatabaseConnectionContext _database;

        public DapperMyEntityRepository(DatabaseConnectionContext database)
        {
            _database = database;
        }

        public Task<MyEntity> Get(int id, CancellationToken cancellationToken)
        {
            return _database.Connection.QueryFirstAsync<MyEntity>("select * from [Table] where [Id] = @id", new {id}, _database.Transaction);
        }

        public async Task<MyEntity> Insert(MyEntity item, CancellationToken cancellationToken)
        {
            var id = await _database.Connection.ExecuteAsync("insert into [Table] (...) values (@Name, @Age); return @@Identity", new { item.Name, item.Age }, _database.Transaction);
            return await Get(id, cancellationToken);
        }

        public async Task<MyEntity> Update(MyEntity item, CancellationToken cancellationToken)
        {
            await _database.Connection.ExecuteAsync("Update [Table] Set [Name] = @Name, [Age] = @Age where [Id] = @id", new { item.Name, item.Age, item.Id }, _database.Transaction);
            return await Get(item.Id, cancellationToken);
        }

        public Task Delete(MyEntity item, CancellationToken cancellationToken)
        {
            return _database.Connection.ExecuteAsync("delete from [Table] where [Id] = @id", new { item.Id }, _database.Transaction);
        }
    }
}