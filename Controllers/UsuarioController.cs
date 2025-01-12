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

        [HttpPatch("update-password")]
        [Authorize]
        public Response<Usuario> UpdatePasswordUsuario(UpdatePassword form)
        {
            return _handle.Handle(form);
        }

        [HttpGet("email")]
        [Authorize]
        public Response<Usuario> GetUsuarioByEmail([FromQuery] string email)
        {
            return _handle.Handle(email);
        }

    }
}

public class UpdatePassword
{
    public required string email { get; set; }
    public required string senhaAtual { get; set; }
    public required string senhaNova { get; set; }
}
