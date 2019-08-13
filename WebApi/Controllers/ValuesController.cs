using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Swashbuckle.Swagger.Annotations;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Values")]
    public class ValuesController 
        : ApiController
    {
        private readonly IRepository _repository;

        public ValuesController(IRepository repository) => _repository = repository;

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "All Values", typeof(IEnumerable<string>))]
        public IEnumerable<string> Get() => _repository.All().Select(entity => entity.Value);

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "A Value", typeof(string))]
        [SwaggerResponse(422, "Validation Errors", typeof(ModelStateDictionary))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unhandled Exception")]
        public string Get([Required]int id) => _repository.Get(id)?.Value;

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Create new Entity", typeof(void))]
        [SwaggerResponse(422, "Validation Errors", typeof(ModelStateDictionary))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unhandled Exception")]
        public void Post(ViewModel model) => _repository.Create(new Entity{Value = model.Value});

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "Update existing Entity", typeof(void))]
        [SwaggerResponse(422, "Validation Errors", typeof(ModelStateDictionary))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unhandled Exception")]
        public void Put([Required]int id, ViewModel model)
        {
            var entity = _repository.Get(id);
            entity.Value = model.Value;

            _repository.Save(entity);
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, "Remove existing Entity", typeof(void))]
        [SwaggerResponse(422, "Validation Errors", typeof(ModelStateDictionary))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unhandled Exception")]
        public void Delete([Required]int id) => _repository.Delete(_repository.Get(id));
    }
}
