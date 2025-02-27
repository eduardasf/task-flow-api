﻿using TaskFlow_API.Domains;
using TaskFlow_API.Shared;

namespace TaskFlow_API.Repositories.IRepositories
{
    public interface ITarefaRepository
    {
        IEnumerable<Tarefa> GetAllTarefas();
        Tarefa? GetTarefaById(Guid id);
        Tarefa AddTarefa(Tarefa tarefa);  
        Tarefa? UpdateTarefa(Tarefa tarefa);
        Response<Tarefa> DeleteTarefa(Guid id);
        ResponsePagination GetFilteredTarefas(PageEvent pageEvent);
        Tarefa? ChangeStatusTarefa(Guid id, bool concluido);
    }
}
