using House.Common.EntityModels.PostgreSQL.Packt.Shared;
using HousingAnalysis.ApiServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace HousingAnalysis.ApiServer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Поля и свойств

        private readonly HouseDbContext _context;

        private readonly DbSet<TEntity> _dbSet;

        #endregion

        #region Методы

        public virtual async Task<TEntity> Create(TEntity item)
        {
            this._dbSet.Add(item);
            await this._context.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity?> FindById(Guid id)
        {
            return await this._dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Get(Predicate<TEntity> predicate)
        {
            var items = new List<TEntity>();
            await foreach (var item in this._dbSet.AsNoTracking().AsAsyncEnumerable())
            {
                if (predicate(item))
                    items.Add(item);
            }

            return items;
        }


        public async Task<TEntity> GetOne(Predicate<TEntity> predicate)
        {
            await foreach (var item in this._dbSet.AsAsyncEnumerable())
            {
                if (predicate(item))
                    return item;
            }

            return null;
        }

        public async Task Remove(TEntity item)
        {
            this._dbSet.Remove(item);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await this._dbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Изменение обновленных полей по сравнению с oldItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public  async Task<TEntity?> Update(Guid id, TEntity item)
        {
            var oldItem = await this._dbSet.FindAsync(id);
            if (oldItem == null)
            {
                return null;
            }
            item.GetType().GetProperty("id")?.SetValue(item,id);
            this._context.Entry(oldItem).CurrentValues.SetValues(item);
            await this._context.SaveChangesAsync();
            return item;
        }

        #endregion

        #region Конструторы

        public GenericRepository(HouseDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        #endregion
    }
}