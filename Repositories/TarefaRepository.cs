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
                query = query.Where(t =>
                    t.Nome.ToLower().Contains(pageEvent.GlobalFilter.ToLower()) ||
                    (!string.IsNullOrEmpty(t.Descricao) && t.Descricao.ToLower().Contains(pageEvent.GlobalFilter.ToLower()))
                );
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

            UpdateOverdueTasks(tarefas);

            return new ResponsePagination
            {
                Success = true,
                Data = tarefas,
                Message = "Tarefas filtradas com sucesso",
                PageEvent = pageEvent
            };
        }

        private void UpdateOverdueTasks(List<Tarefa> tarefas)
        {
            var overdueTasks = tarefas.Where(t => t.DataValidade < DateTime.Now && t.Status != StatusTarefa.Completed).ToList();
            if (overdueTasks.Any())
            {
                foreach (var task in overdueTasks)
                {
                    task.Status = StatusTarefa.Overdue;
                }
                _context.SaveChanges();
            }
        }

        public Tarefa? ChangeStatusTarefa(Guid id, bool concluido)
        {
            var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return null;
            }

            DateTime dataAtual = DateTime.Now;

            tarefa.Concluido = concluido;

            if (concluido)
            {
                tarefa.Status = StatusTarefa.Completed;
            }
            else
            {
                tarefa.Status = dataAtual > tarefa.DataValidade
                    ? StatusTarefa.Overdue
                    : StatusTarefa.Pending;
            }

            _context.SaveChanges();
            return tarefa;
        }

    }
}
