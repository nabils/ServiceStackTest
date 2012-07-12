using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    /// <summary>
    /// In-memory repository, so we can run the TODO app without any external dependencies
    /// Registered in Funq as a singleton, auto injected on every request
    /// </summary>
    public class TodoRepository
    {
        private readonly List<Todo> todos = new List<Todo>();

        public List<Todo> GetAll()
        {
            return todos;
        }

        public Todo GetById(long id)
        {
            return todos.FirstOrDefault(x => x.Id == id);
        }

        public Todo Store(Todo todo)
        {
            if (todo.Id == default(long))
            {
                todo.Id = todos.Count == 0 ? 1 : todos.Max(x => x.Id) + 1;
            }
            else
            {
                for (var i = 0; i < todos.Count; i++)
                {
                    if (todos[i].Id != todo.Id) continue;

                    todos[i] = todo;
                    return todo;
                }
            }

            todos.Add(todo);
            return todo;
        }

        public void DeleteById(long id)
        {
            todos.RemoveAll(x => x.Id == id);
        }
    }
}