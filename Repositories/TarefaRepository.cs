using TaskFlow_API.Data;
using TaskFlow_API.Domains;
using TaskFlow_API.Repositories.IRepositories;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }
        public Tarefa AddTarefa(Tarefa tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return tarefa;
        }

        public Response<Tarefa> DeleteTarefa(Guid id)
        {
            var tarefa = _context.Tarefas
                .FirstOrDefault(t => t.Id == id);

            if(tarefa == null)
            {
                return new Response<Tarefa>
                {
                    Success = false,
                    Message = $"Não foi possível excluír a tarefa. Tarefa não encontrada.",
                    Data = null
                };
            }

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return new Response<Tarefa>
            {
                Success = true,
                Message = $"Tarefa excluída com sucesso",
                Data = null
            };
        }

        public IEnumerable<Tarefa> GetAllTarefas()
        {
            return _context.Tarefas
                .ToList();
        }

        public Tarefa? GetTarefaById(Guid id)
        {
            var tarefa = _context.Tarefas
                .FirstOrDefault(t => t.Id == id);

            if (tarefa == null)
            {
                return null;
            }

            return tarefa;
        }


        public Tarefa? UpdateTarefa(Guid id, Tarefa tarefa)
        {
            var tarefaExistente = _context.Tarefas.Find(id);

            if (tarefaExistente == null)
            {
                return null;
            }

            tarefaExistente.Nome = tarefa.Nome;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.DataValidade = tarefa.DataValidade;
            tarefa.Concluido = tarefa.Concluido;

            _context.Tarefas.Update(tarefaExistente);
            _context.SaveChanges();

            return tarefa; 
        }

    }
}
