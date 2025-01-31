using AngularAuthAPI.Models;

namespace AngularAuthAPI.Interface
{
    public interface ITodoInterface
    {
        public Todo AddTodo(string title,string description,int userid);
        public Task<List<Todo>> ReadTodo(int userid);
        public Task<Todo> EditTodoDesc(int userid,string title,string desciption);
        public Task<bool> DeleteTodo(int userid,string title);
        public Task<bool> TodoAlreadyExist(string title,int userid);

    }
}
