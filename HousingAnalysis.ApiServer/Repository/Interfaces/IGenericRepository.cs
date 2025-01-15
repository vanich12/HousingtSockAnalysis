namespace HousingAnalysis.ApiServer.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Создание
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<TEntity> Create(TEntity item);

        /// <summary>
        /// Поиск обьекта по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> FindById(Guid id);

        /// <summary>
        /// Извлечение всех обьектов
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get(Predicate<TEntity> predicate);

        /// <summary>
        /// Поиск одного обекта по условию
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> GetOne(Predicate<TEntity> predicate);

        /// <summary>
        /// Удаление обьекта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task Remove(TEntity item);

        /// <summary>
        /// Получение всех обьектов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Get();

        /// <summary>
        /// Обновление обьекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<TEntity?> Update(Guid id, TEntity item);
    }
}