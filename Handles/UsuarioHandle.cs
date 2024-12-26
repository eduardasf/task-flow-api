using TaskFlow_API.Domains;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Handles
{
    public class UsuarioHandle
    {
        private readonly IUsuarioRepository _usuario;

        public UsuarioHandle(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        public Response<Usuario> Handle(Usuario usuario)
        {
            if (usuario == null)
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = $"Não foi possível criar um usuário.",
                    Data = null

                };
            }

            var data = _usuario.AddUsuario(usuario);

            return new Response<Usuario>
            {
                Success = true,
                Message = $"Usuário criado com sucesso!",
                Data = data
            };
        }

        public Response<Usuario> Handle(Guid id)
        {
            var usuario = id == Guid.Empty
                ? null
                : _usuario.GetUsuarioById(id);

            return new Response<Usuario>
            {
                Success = usuario != null,
                Message = usuario != null
                    ? "Usuário encontrada com sucesso!"
                    : $"Não foi possível encontrar a usuário com o id {id}.",
                Data = usuario
            };
        }

        public Response<Usuario> Handle(string email, string senhaAtual, string senhaNova)
        {
            var response = _usuario.UpdatePasswordUsuario(email, senhaAtual, senhaNova);

            return response;
        }
    }
}
