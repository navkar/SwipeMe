using System;
using SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using DataLib2.Models;

namespace DataLib2.DAL
{
    public class NotesDBManager
    {
        SQLiteAsyncConnection databaseConnection = DBConnectionMgr.Instance.databaseConnection;
        DataBaseManager<NotesModel> dbManager;

        #region Constructor
        public NotesDBManager()
        {
            dbManager = new DataBaseManager<NotesModel>(databaseConnection);
            CreateTableIfNotExists();
        }
        #endregion

        #region Private methods

        private async void CreateTableIfNotExists()
        {
            var exists = await DBConnectionMgr.Instance.DoesTableExist(DBConstants.Notes);
            if (!exists)
            {
                try
                {
                    var response = await databaseConnection.CreateTableAsync<NotesModel>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        #endregion

        #region Public methods
        public async Task<DateTime> GetCachedDateAsync(string id)
        {
            try
            {
                var result = await databaseConnection.Table<NotesModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
                return result.CachedDate;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.InnerException);
            }
            return DateTime.Now;
        }

        public async Task<int> SaveItemAsync(NotesModel item)
        {
            try
            {
                var result = await dbManager.Insert(item);
                return result;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.InnerException);
            }
            return 0;
        }

        public async Task<int> UpdateItemAsync(NotesModel item)
        {
            try
            {
                var result = await dbManager.Update(item);
                return result;

            }
            catch (Exception err)
            {
                Debug.WriteLine(err.InnerException);
            }
            return 0;
        }

        public async Task<int> DeleteItemAsync(NotesModel item)
        {
            try
            {
                var result = await dbManager.Delete(item);
                return result;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.InnerException);
            }
            return 0;
        }

        public async Task<NotesModel> Get(string id)
        {
            NotesModel result = null;
            try
            {
                result = await databaseConnection.Table<NotesModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        #endregion
    }
}

