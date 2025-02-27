﻿using TaskFlow_API.Domains;
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

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            var data = _usuario.AddUsuario(usuario);

            return data;
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

        public Response<Usuario> Handle(UpdatePassword form)
        {
            if (string.IsNullOrWhiteSpace(form.email) || string.IsNullOrWhiteSpace(form.senhaAtual) ||
                string.IsNullOrWhiteSpace(form.senhaNova))
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = "E-mail, senha atual e nova senha são obrigatórios.",
                    Data = null
                };
            }
            return _usuario.UpdatePasswordUsuario(form);
        }

        public Response<Usuario> Handle(string email)
        {
            if (email == null)
            {
                return new Response<Usuario>
                {
                    Success = false,
                    Message = $"Não foi possível buscar o usuário com o email especificado.",
                    Data = null
                };
            }

            var data = _usuario.GetUsuarioByEmail(email);
            data.Password = null;

            return new Response<Usuario>
            {
                Success = true,
                Message = $"Usuário encontrado com sucesso!",
                Data = data
            };
        }
    }
}