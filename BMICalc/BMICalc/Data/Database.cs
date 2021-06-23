using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMICalc.Data
{
    //This is the User-Database
    public class UserDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserData>().Wait();
        }

        //Returns a list of all usernames in the DB
        public List<string> GetUsers()
        {
            Task<List<UserData>> dbData = _database.Table<UserData>().ToListAsync();

            List<string> users = new List<string>();

            foreach (var user in dbData.Result)
            {
                users.Add(user.Name);
            }

            return users;
        }

        //Returns the Object of a specific user
        public Task<UserData> GetUser(string Name)
        {
            return _database.Table<UserData>().Where(x => x.Name == Name).FirstOrDefaultAsync();
        }

        //Removes a specific user from the DB
        public Task<int> RemoveUser(UserData User)
        {
            return _database.DeleteAsync(User);
        }

        //Adds a new user to the DB
        public Task<int> AddUser(UserData UserName)
        {
            return _database.InsertAsync(UserName);
        }

        //Clears the whole DB
        public void ClearDB()
        {
            _database.DeleteAllAsync<UserData>();
        }
    }

    //This is the BMI-Database
    public class BmiDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public BmiDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<BMIData>().Wait();
        }

        //Gets a list of all saved BMIs
        public Task<List<BMIData>> GetBMIs()
        {
            return _database.Table<BMIData>().ToListAsync();
        }

        //Gets the saved BMI-Data of a specific user
        public Task<List<BMIData>> GetBMIForName(string Name)
        {
            return _database.Table<BMIData>().Where(x => x.Name == Name).ToListAsync();
        }

        //Saves a new BMI value to the Database
        public Task<int> SaveNewRecord(BMIData Data)
        {
            return _database.InsertAsync(Data);
        }

        //Clears the whole DB
        public void ClearDB()
        {
            _database.DeleteAllAsync<BMIData>();
        }
    }
}
