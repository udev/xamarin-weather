using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather
{
	public interface ITodoService
	{
		Task<List<TodoItem>> RefreshDataAsync ();

		Task SaveTodoItemAsync (TodoItem item, bool isNewItem);

		Task DeleteTodoItemAsync (string id);
	}
}
