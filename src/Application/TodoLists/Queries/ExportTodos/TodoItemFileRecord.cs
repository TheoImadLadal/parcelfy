using parcelfy.Application.Common.Mappings;
using parcelfy.Domain.Entities;

namespace parcelfy.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
