using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public Response<Usuario> GetUsuarioById([FromRoute]Guid id)
        {
            return _handle.Handle(id);
        }

        [HttpPatch]
        [Authorize]
        public Response<Usuario> UpdatePasswordUsuario(string email, string senhaAtual, string senhaNova)
        {
            return _handle.Handle(email, senhaAtual, senhaNova);
        }

        [HttpGet("email")]
        [Authorize]
        public Response<Usuario> GetUsuarioByEmail([FromQuery] string email)
        {
            return _handle.Handle(email);
        }

    }
}
