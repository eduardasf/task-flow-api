using TaskFlow_API.Domains;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Repositories.IRepositories
{
    public interface IUsuarioRepository
    {
        Usuario AddUsuario(Usuario usuario);
        Usuario? GetUsuarioById(Guid id);
        Response<Usuario> UpdatePasswordUsuario(string email, string senhaAtual, string senhaNova);
        Usuario? GetUsuarioByEmail(string email);
    }
}
