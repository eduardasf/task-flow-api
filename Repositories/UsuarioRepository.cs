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
        public Response<Usuario> AddUsuario(Usuario usuario)
        {
            var usuarioExistente = GetUsuarioByEmail(usuario.Email);

            if (usuarioExistente != null)
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = "Esse e-mail já está cadastrado.",
                    Data = null
                };
            }

            usuario.Id = Guid.NewGuid();
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return new Response<Usuario>
            {
                Success = true,
                Message = "Usuário criado com sucesso!",
                Data = usuario
            };
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

        public Response<Usuario> UpdatePasswordUsuario(UpdatePassword form)
        {

            var usr = _context.Usuarios
                .FirstOrDefault(u => u.Email == form.email);

            if (usr == null)
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Data = null
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(form.senhaAtual, usr.Password))
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = "Senha atual incorreta.",
                    Data = null
                };
            }

            usr.Password = BCrypt.Net.BCrypt.HashPassword(form.senhaNova);
            _context.SaveChanges();
            usr.Password = null;

            return new Response<Usuario>
            {
                Success = true,
                Message = "Senha alterada com sucesso!",
                Data = usr
            };
        }

        public Usuario? GetUsuarioByEmail(string email)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email);

            if(usuario == null)
            {
                return null;
            }

            return usuario;
        }
    }
}