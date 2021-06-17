using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMICalc.Data
{
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserData>().Wait();
        }

        public Task<List<UserData>> GetUsers()
        {
            return _database.Table<UserData>().ToListAsync();
        }

        public Task<UserData> GetUser(string Name)
        {
            return _database.Table<UserData>().Where(x => x.Name == Name).FirstOrDefaultAsync();
        }

        public Task<int> RemoveUser(UserData User)
        {
            return _database.DeleteAsync(User);
        }

        public Task<int> AddUser(UserData UserName)
        {
            return _database.InsertAsync(UserName);
        }

        public void ClearDB()
        {
            _database.DeleteAllAsync<UserData>();
        }
    }

    public class BmiDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public BmiDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<BMIData>().Wait();
        }

        public Task<List<BMIData>> GetBMIs()
        {
            return _database.Table<BMIData>().ToListAsync();
        }

        public Task<List<BMIData>> GetBMIForName(string Name)
        {
            return _database.Table<BMIData>().Where(x => x.Name == Name).ToListAsync();
        }

        public Task<int> SaveNewRecord(BMIData Data)
        {
            return _database.InsertAsync(Data);
        }

        public void ClearDB()
        {
            _database.DeleteAllAsync<BMIData>();
        }
    }
}
