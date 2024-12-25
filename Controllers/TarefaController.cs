using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Domains;
using TaskFlow_API.Handles;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Controllers
{

    [ApiController]
    [Route("api/tarefa")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaHandle _handle;

        public TarefaController(TarefaHandle handle)
        {
            _handle = handle;
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

        [HttpPut("{id}")]
        public Response<Tarefa> UpdateTarefa([FromRoute] Guid id, [FromBody] Tarefa tarefa)
        {
            return _handle.Handle(id, tarefa);
        }

        [HttpDelete("{id}")]
        public Response<Tarefa> DeleteTarefa([FromRoute] Guid id)
        {
            return _handle.HandleDelete(id);
        }

    }
}
