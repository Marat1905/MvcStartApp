using Microsoft.EntityFrameworkCore;
using MvcStartApp.Context;
using MvcStartApp.Interfaces;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;

        // Метод-конструктор для инициализации
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
           return await _context.Users.ToArrayAsync();
        }
    }
}
