namespace AngularAuthAPI.Interface
{
    public interface IUnitOfWork
    {
        ITodoInterface TodoRepository { get; }
        IUserRepository UserRepository { get; }
        public Task<bool> SaveAsync();
    }
}
