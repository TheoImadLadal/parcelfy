using parcelfy.Application.TodoLists.Queries.ExportTodos;

namespace parcelfy.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
