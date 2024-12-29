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
            tarefa.Status = StatusTarefa.Pending;
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

        public Tarefa? UpdateTarefa(Tarefa tarefa)
        {
            var tarefaExistente = _context.Tarefas.Find(tarefa.Id);

            if (tarefaExistente == null)
            {
                return null;
            }
            tarefaExistente.Nome = tarefa.Nome;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.DataValidade = tarefa.DataValidade;
            tarefaExistente.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaExistente);
            _context.SaveChanges();

            return tarefaExistente;
        }

        public ResponsePagination GetFilteredTarefas(PageEvent pageEvent)
        {
            var query = _context.Tarefas.AsQueryable();

            if (!string.IsNullOrEmpty(pageEvent.GlobalFilter))
            {
                query = query.Where(t => t.Nome.ToLower().Contains(pageEvent.GlobalFilter.ToLower())
                                         || (t.Descricao != null && t.Descricao.ToLower().Contains(pageEvent.GlobalFilter.ToLower())));
            }

            if (pageEvent.Status != StatusTarefa.All)
            {
                query = query.Where(t => t.Status == pageEvent.Status);
            }

            pageEvent.Total = query.Count();

            var tarefas = query
                .Skip(pageEvent.First ?? 0)
                .Take(pageEvent.Rows)
                .ToList();

            return new ResponsePagination
            {
                Success = true,
                Data = tarefas,
                Message = "Tarefas filtradas com sucesso",
                PageEvent = pageEvent
            };
        }


    }
}
