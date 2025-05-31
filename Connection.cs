using System;
using System.Threading.Tasks;
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
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
                System.Diagnostics.Debug.WriteLine($"Database path: {dbPath}");
                connection = new SQLiteAsyncConnection(dbPath);
                connection.CreateTableAsync<TaskModel>().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Connection error: {ex.Message}");
                throw;
            }
        }

        public async Task<System.Collections.Generic.List<TaskModel>> GetTasksAsync()
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            return await connection.Table<TaskModel>().ToListAsync();
        }

        public async Task SaveTaskAsync(TaskModel task)
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            if (task == null)
            {
                System.Diagnostics.Debug.WriteLine("SaveTaskAsync: Task is null");
                throw new ArgumentNullException(nameof(task));
            }
            if (task.Id == 0)
            {
                System.Diagnostics.Debug.WriteLine($"Inserting new task: Name={task.TaskName}, Status={task.TaskStatus}");
                await connection.InsertAsync(task);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Updating task: Id={task.Id}, Name={task.TaskName}, Status={task.TaskStatus}");
                await connection.UpdateAsync(task);
            }
        }

        public async Task DeleteTaskAsync(TaskModel task)
        {
            if (connection == null)
                throw new InvalidOperationException("Database connection is not initialized.");
            if (task == null)
            {
                System.Diagnostics.Debug.WriteLine("DeleteTaskAsync: Task is null");
                throw new ArgumentNullException(nameof(task));
            }
            System.Diagnostics.Debug.WriteLine($"Deleting task: Id={task.Id}, Name={task.TaskName}");
            await connection.DeleteAsync(task);
        }
    }
}