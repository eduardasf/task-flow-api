using Microsoft.AspNetCore.Mvc;
using TaskFlow_API.Domains;
using TaskFlow_API.Handles;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController: ControllerBase
    {
        private readonly UsuarioHandle _handle;

        public UsuarioController(UsuarioHandle handle)
        {
            _handle = handle;
        }

        [HttpPost]
        public Response<Usuario> AddUsuario([FromBody]Usuario usuario)
        {
            return _handle.Handle(usuario);
        }

        [HttpGet("{id}")]
        public Response<Usuario> GetUsuarioById([FromRoute]Guid id)
        {
            return _handle.Handle(id);
        }

        [HttpPatch]
        public Response<Usuario> UpdatePasswordUsuario(string email, string senhaAtual, string senhaNova)
        {
            return _handle.Handle(email, senhaAtual, senhaNova);
        }

        [HttpGet("email")]
        public Response<Usuario> GetUsuarioByEmail([FromQuery] string email)
        {
            return _handle.Handle(email);
        }

    }
}
