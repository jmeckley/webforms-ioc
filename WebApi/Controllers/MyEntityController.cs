using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/MyEntity")]
    public class MyEntityController 
        : ApiController
    {
        private readonly IRepository<MyEntity> _repository;

        public MyEntityController(IRepository<MyEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<MyEntity> Get([Required, Range(0,int.MaxValue)]int id, CancellationToken cancellationToken)
        {
            return _repository.Get(id, cancellationToken);
        }

        [HttpPost]
        [Route("")]
        [UnitOfWork]
        public Task<MyEntity> Post(MyEntityViewModel model, CancellationToken cancellationToken)
        {
            var entity = new MyEntity
            {
                Name = model.Name,
                Age = model.Age
            };

            return _repository.Insert(entity, cancellationToken);
        }

        [HttpPut]
        [Route("{id}")]
        [UnitOfWork]
        public async Task<MyEntity> Put([Required, Range(0, int.MaxValue)]int id, MyEntityViewModel model, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(id, cancellationToken);
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Age = model.Age;
            }

            return await _repository.Update(entity, cancellationToken);
        }

        [HttpDelete]
        [Route("{id}")]
        [UnitOfWork]
        public async Task Delete([Required, Range(0, int.MaxValue)]int id, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(id, cancellationToken);
            await _repository.Delete(entity, cancellationToken);
        }
    }
}
