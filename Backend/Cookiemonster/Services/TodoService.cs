using Cookiemonster.Models;


namespace Cookiemonster.Services
{
    public class TodoService
    {
        private readonly Repository<Todo> _todoRepository;

        public TodoService(Repository<Todo> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Todo CreateTodo(Todo todo)
        {
            return _todoRepository.Create(todo);
        }

        public Todo GetTodo(int userId, int recipeId)
        {
            return _todoRepository.Get(userId, recipeId);
        }

        public List<Todo> GetAllTodos()
        {
            return _todoRepository.GetAll();
        }

        public bool DeleteTodo(int userId, int recipeId)
        {
            return _todoRepository.Delete(userId, recipeId);
        }
    }
}
