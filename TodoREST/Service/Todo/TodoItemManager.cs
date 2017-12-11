﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather
{
	public class TodoItemManager
	{
		ITodoService restService;

		public TodoItemManager (ITodoService service)
		{
			restService = service;
		}

		public Task<List<TodoItem>> GetTasksAsync ()
		{
			return restService.RefreshDataAsync ();	
		}

		public Task SaveTaskAsync (TodoItem item, bool isNewItem = false)
		{
			return restService.SaveTodoItemAsync (item, isNewItem);
		}

		public Task DeleteTaskAsync (TodoItem item)
		{
			return restService.DeleteTodoItemAsync (item.ID);
		}
	}
}
