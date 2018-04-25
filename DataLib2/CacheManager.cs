using DataLib2.DAL;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DataLib2
{
    public class CacheManager
    {
        public static CacheManager Instance => Singleton<CacheManager>.Instance;

        public async Task<string> ReturnCachedResponse(string tableName, string requestId)
        {
            try
            {
                switch (tableName)
                {
                    case DBConstants.Notes:
                        var mgr = new NotesDBManager();
                        var records = await mgr.Get(requestId);
                        if (records != null) 
                        {
                           return JsonConvert.SerializeObject(records);
                        }
                        break;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetCachedDateForTable(string tableName, string requestId)
        {
            try
            {
                switch (tableName)
                {
                    case DBConstants.Notes:
                        var mgr = new NotesDBManager();
                        var date = await mgr.GetCachedDateAsync(requestId);
                        if (date != null)
                        {
                            return date.ToString();
                        }
                        break;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
