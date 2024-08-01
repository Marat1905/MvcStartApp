using MvcStartApp.Models.DB;

namespace MvcStartApp.Interfaces
{
    public interface IBlogRepository
    {
        Task AddUser(User user);

        Task<User[]> GetUsers();
    }
}
