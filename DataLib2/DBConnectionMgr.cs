using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace DataLib2
{
    public class DBConnectionMgr
    {
        static DBConnectionMgr _instance;
        public static DBConnectionMgr Instance
        {
            get { return _instance ?? (_instance = new DBConnectionMgr()); }
        }

        public readonly SQLiteAsyncConnection databaseConnection;
        
        public DBConnectionMgr()
        {
            try
            {
                string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath(DBConstants.DB_NAME);
                databaseConnection = new SQLiteAsyncConnection(dbPath);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.InnerException);
            }
        }

        public async Task<bool> DoesTableExist(string tableName)
        {
            try
            {
                var tableExistsQuery = string.Format("SELECT name FROM sqlite_master WHERE type='table' AND name='{0}';", tableName);
                var result = await databaseConnection.ExecuteScalarAsync<string>(tableExistsQuery);
                Debug.WriteLine("result: " + result);
                return (result != null);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
