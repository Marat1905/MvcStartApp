using MvcStartApp.Models.DB;

namespace MvcStartApp.Interfaces
{
    public interface IRequestsRepository
    {
        /// <summary>Добавить логгирование запросов</summary>
        /// <param name="request">модель запросов</param>
        /// <returns></returns>
        Task AddRequestsAsync(Request request);


        /// <summary>Получить все запросы </summary>
        /// <returns>Перечисление с БД<</returns>
        Task<IEnumerable<Request>> GetAllRequestsAsync();
    }
}
