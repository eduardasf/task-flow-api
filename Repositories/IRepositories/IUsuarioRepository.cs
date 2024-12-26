using TaskFlow_API.Domains;

namespace TaskFlow_API.Repositories.IRepositories
{
    public interface IUsuarioRepository
    {
        Usuario AddUsuario(Usuario usuario);
        Usuario? GetUsuarioById(Guid id);
       // Usuario UpdateUsuario(Guid id, Usuario usuario);
    }
}
