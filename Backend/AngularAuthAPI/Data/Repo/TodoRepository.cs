using AngularAuthAPI.Interface;
using AngularAuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Data.Repo
{
    public class TodoRepository : ITodoInterface
    {
        public readonly DBC dbc;

        public TodoRepository(DBC dbc)
        {
            this.dbc = dbc;
        }

        public Todo AddTodo(string title, string description,int userid)
        {
            Todo todo = new Todo();
            todo.Title = title;
            todo.Description = description;
            todo.UserID = userid;
            todo.CreatedOn = DateTime.Now;
            todo.LastEdited = DateTime.Now;
            dbc.Todos.Add(todo);
            return todo;
        }

        public async Task<bool> DeleteTodo(int userid, string title)
        {
            var todo = await dbc.Todos.FirstOrDefaultAsync(x => x.UserID == userid && x.Title == title);
            if(todo == null)
            {
                return false;
            }
            dbc.Todos.Remove(todo);
            return true;
        }

        public async Task<Todo> EditTodoDesc(int userid,string title,string description)
        {
            var todo = await dbc.Todos.FirstOrDefaultAsync(x => x.UserID == userid && x.Title == title);
            if (todo == null)
            {
                return null;
            }
            todo.Description = description;
            todo.LastEdited= DateTime.Now;
            dbc.Todos.Update(todo);
            return todo;
        }

       
        public async Task<List<Todo>> ReadTodo(int userid)
        {
            return await dbc.Todos.Where(x=>x.UserID==userid).ToListAsync();
        }

        public async Task<bool> TodoAlreadyExist(string title,int userid)
        {
            var todo=await dbc.Todos.FirstOrDefaultAsync(x => x.Title == title&&x.UserID==userid);
            if (todo == null)
            {
                return false;
            }
            return true;
        }


    }
}
