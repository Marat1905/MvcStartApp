using Microsoft.EntityFrameworkCore;
using MvcStartApp.Context;
using MvcStartApp.Interfaces;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Repositories
{
    public class RequestsRepository : IRequestsRepository
    {
        private readonly BlogContext _context;

        public RequestsRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task AddRequestsAsync(Request request)
        {
            var entry = _context.Entry(request);

            if (entry.State == EntityState.Detached)
            {
                await _context.Requests.AddAsync(request);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _context.Requests.ToListAsync();
        }
    }
}
