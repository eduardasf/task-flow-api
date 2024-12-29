using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Domains;
using TaskFlow_API.Handles;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Controllers
{

    [ApiController]
    [Route("api/tarefa")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaHandle _handle;
        private readonly ITarefaRepository _repository;

        public TarefaController(TarefaHandle handle, ITarefaRepository repository)
        {
            _handle = handle;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public Response<Tarefa> GetTarefaById([FromRoute] Guid id)
        {
            var response = _handle.Handle(id);
            return response;
        }

        [HttpPost]
        public Response<Tarefa> AddTarefa([FromBody] Tarefa tarefa)
        {
            return _handle.Handle(tarefa);
        }

        [HttpPut]
        public Response<Tarefa> UpdateTarefa([FromBody] Tarefa tarefa)
        {
            return _handle.HandleUpdateTarefa(tarefa);
        }

        [HttpDelete("{id}")]
        public Response<Tarefa> DeleteTarefa([FromRoute] Guid id)
        {
            return _handle.HandleDelete(id);
        }

        [HttpGet]
        public IEnumerable<Tarefa> GetAllTarefas()
        {
            return _repository.GetAllTarefas();      
        }

        [HttpPost("pagination")]
        public ResponsePagination GetPagination([FromBody]PageEvent pageEvent)
        {
            return _repository.GetFilteredTarefas(pageEvent);
        }

    }
}
