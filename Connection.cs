using SQLite;

namespace PetProject
{
    public partial class Connection
    {
        private const string DB_NAME = "tasks.db3";
        private readonly SQLiteAsyncConnection connection;

        public Connection()
        {
            try
            {
                connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
                connection.CreateTableAsync<TaskModel>().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connection error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<TaskModel>> GetTasksAsync()
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            return await connection.Table<TaskModel>().ToListAsync();
        }

        public async Task SaveTaskAsync(TaskModel task)
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            await connection.InsertAsync(task);
        }

        public async Task DeleteTaskAsync(TaskModel task)
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            await connection.DeleteAsync(task);
        }
    }
}