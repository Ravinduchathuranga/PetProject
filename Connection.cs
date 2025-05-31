using SQLite;

namespace PetProject
{
    public partial class Connection
    {
        private const string DB_NAME = "taks.db3";
        private readonly SQLiteAsyncConnection connection;

        public Connection()
        {
            connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DB_NAME));
            connection.CreateTableAsync<TaskModel>();
        }
        public async Task<List<TaskModel>> GetTasksAsync()
        {
            return await connection.Table<TaskModel>().ToListAsync();
        }

        public async Task<int> SaveTaskAsync(TaskModel task)
        {
            if (task.Id != 0)
                return await connection.UpdateAsync(task);
            return await connection.InsertAsync(task);
        }

        public async Task DeleteTaskAsync(TaskModel task)
        {
            await connection.DeleteAsync(task);
        }
    }
}
