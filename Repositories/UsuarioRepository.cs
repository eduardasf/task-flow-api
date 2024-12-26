using TaskFlow_API.Data;
using TaskFlow_API.Domains;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public Usuario AddUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario? GetUsuarioById(Guid id)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Id == id);

            if(usuario == null)
            {
                return null;
            }

            return usuario;
        } 

        public Response<Usuario> UpdatePasswordUsuario(string email, string senhaAtual, string senhaNova)
        {

            var usuarioExistente = _context.Usuarios
                .FirstOrDefault(u => u.Email == email);

            if (usuarioExistente == null)
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = $"Não foi possível realizar a atualização da senha! Usuário não encotrado.",
                    Data = null
                };
            }

            if (usuarioExistente.Password == senhaAtual)
            {
                usuarioExistente.Password = senhaNova;
                _context.SaveChanges();
            }
            else
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = $"Não foi possível realizar a atualização da senha! Senha atual incorreta.",
                    Data = null
                };
            }

            return new Response<Usuario>
            {
                Success = true,
                Message = $"Senha atualizado com sucesso!",
                Data = null
            };
        }
    }
}
