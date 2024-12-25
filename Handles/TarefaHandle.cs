using TaskFlow_API.Data;
using TaskFlow_API.Domains;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Handles
{
    public class TarefaHandle
    {
        private readonly ITarefaRepository _tarefa;

        public TarefaHandle(ITarefaRepository tarefa)
        {
            _tarefa = tarefa;
        }

        public Response<Tarefa> Handle(Guid id)
        {
            var tarefa = id == Guid.Empty
                ? null
                : _tarefa.GetTarefaById(id);

            return new Response<Tarefa>
            {
                Success = tarefa != null,
                Message = tarefa != null
                    ? "Tarefa encontrada com sucesso!"
                    : $"Não foi possível encontrar a Tarefa com o id {id}.",
                Data = tarefa
            };
        }

        public Response<Tarefa> Handle(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return new Response<Tarefa>
                {
                    Success = false,
                    Message = $"Não foi possível criar a tarefa.",
                    Data = null

                };
            }

            var data = _tarefa.AddTarefa(tarefa);

            return new Response<Tarefa>
            {
                Success = true,
                Message = $"Tarefa criada com sucesso!",
                Data = data
            };
        }

        public Response<Tarefa> Handle(Guid id, Tarefa tarefa)
        {
            if(id == Guid.Empty)
            {
                return new Response<Tarefa>
                {
                    Success = false,
                    Message = $"Não foi possível atualizar a tarefa. Id nulo.",
                    Data = null
                };
            }

            var data = _tarefa.UpdateTarefa(id, tarefa);

            if (data == null)
            {
                return new Response<Tarefa>
                {
                    Success = false,
                    Message = $"Não foi possível encontrar a tarefa para ser atualizada.",
                    Data = null
                };
            }

            return new Response<Tarefa>
            {
                Success = true,
                Message = $"Tarefa atualizada com sucesso!",
                Data = data
            };
        }

        public Response<Tarefa> HandleDelete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new Response<Tarefa>
                {
                    Success = false,
                    Message = $"Não foi possível atualizar a tarefa. Id nulo.",
                    Data = null
                };
            }

           var response =  _tarefa.DeleteTarefa(id);

           return response;
        }

    }
}
