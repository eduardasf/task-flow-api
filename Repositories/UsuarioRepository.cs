using TaskFlow_API.Data;
using TaskFlow_API.Domains;
using TaskFlow_API.Repositories.IRepositories;

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

        //public Usuario? UpdateUsuario(Guid id, Usuario usuario)
        //{
        //    var usuarioExistente = _context.Usuarios
        //        .FirstOrDefault (u => u.Id == id);

        //    if (usuarioExistente == null)
        //    {
        //        return null;
        //    }

        //    usuarioExistente.Name = usuario.Name;
        //    usuarioExistente.Password = usuario.Password;
        //    usuarioExiste
        //}
    }
}
