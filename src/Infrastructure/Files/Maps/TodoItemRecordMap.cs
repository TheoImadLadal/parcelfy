using System.Globalization;
using parcelfy.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;

namespace parcelfy.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
    }
}
