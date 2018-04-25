using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLib2
{
    public class DataBaseManager<T> : IDataBaseManager<T> where T : class, new()
    {
        private SQLiteAsyncConnection dbConnection;

        public DataBaseManager(SQLiteAsyncConnection db)
        {
            dbConnection = db;
        }

        public AsyncTableQuery<T> AsQueryTable() => dbConnection.Table<T>();

        /// <summary>
        /// create table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CreateTablesResult> CreateTable(T entity)
        {
            return await dbConnection.CreateTableAsync<T>();
        }
        
        /// <summary>
        /// to get list of items from table
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> Get() => await dbConnection.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            try
            {
                var query = dbConnection.Table<T>();

                if (predicate != null)
                    query = query.Where(predicate);

                if (orderBy != null)
                    query = query.OrderBy<TValue>(orderBy);

                return await query.ToListAsync();
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.InnerException);
            }
            return null;
        }

        /// <summary>
        /// get element by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> Get(int id) => await dbConnection.FindAsync<T>(id);

        /// <summary>
        /// get element by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await dbConnection.FindAsync<T>(predicate);
        
        /// <summary>
        /// Insert element to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Insert(T entity) =>
             await dbConnection.InsertAsync(entity);

        /// <summary>
        /// Insert all elements to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAll(List<T> entity) =>
             await dbConnection.InsertAllAsync(entity);

        /// <summary>
        /// Update table row
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Update(T entity) =>
             await dbConnection.UpdateAsync(entity);

        /// <summary>
        /// delete from table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> Delete(T entity) =>
             await dbConnection.DeleteAsync(entity);

        /// <summary>
        /// delete from table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAll(string entity)
        {
            try
            {
                var deleteQuery = string.Format("DELETE FROM '{0}';", entity);
                var result = await dbConnection.ExecuteAsync(deleteQuery);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
