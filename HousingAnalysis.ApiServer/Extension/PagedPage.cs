using Microsoft.EntityFrameworkCore;

namespace HousingAnalysis.ApiServer.Extension
{
    public class PagedPage<T> where T : class
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Общее число страниц
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Общее число страниц
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Показатель наличия предыдущей страницы
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// показатель наличия следующей старницы
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        ///  Список объектов для которых производится пагинация
        /// </summary>
        public List<T> Data { get; private set; }

        public PagedPage(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.Data = items;
            this.CurrentPage = pageNumber;
            this.TotalCount = count;
            this.PageSize = pageSize;
            this.TotalPages = TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Пагинация
        /// </summary>
        /// <typeparam name="OrderKey"></typeparam>
        /// <param name="source">Объект пагинации</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="orderKey"></param>
        /// <returns>Пагинированные данные</returns>
        public static async Task<PagedPage<T>> ToPagedPage<OrderKey>(IQueryable<T> source, int pageNumber, int pageSize,
            Func<T, OrderKey> orderKey = null)
        {
            var count = source.Count();
            var items = await source.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedPage<T>(items.OrderBy(orderKey).ToList(), count, pageNumber, pageSize);
        }
    }
}