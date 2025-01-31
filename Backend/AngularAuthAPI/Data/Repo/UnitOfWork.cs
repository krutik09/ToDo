using AngularAuthAPI.Interface;

namespace AngularAuthAPI.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBC dbc;

        public UnitOfWork(DBC _dbc)
        {
            dbc = _dbc;
        }
        public IUserRepository UserRepository => new UserRepository(dbc);

        public ITodoInterface TodoRepository => new TodoRepository(dbc);

        public async Task<bool> SaveAsync()
        {
            return await dbc.SaveChangesAsync()>0;
        }
    }
}
