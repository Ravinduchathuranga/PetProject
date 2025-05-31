using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SQLite;

namespace PetProject.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly Connection _connection;
        private System.Collections.ObjectModel.ObservableCollection<TaskModel> _tasks;

        public System.Collections.ObjectModel.ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand UpdateTaskStatusCommand { get; }

        public MainPageViewModel()
        {
            _connection = new Connection();
            Tasks = new System.Collections.ObjectModel.ObservableCollection<TaskModel>();
            AddNoteCommand = new Command(async () => await OnAddNote());
            DeleteNoteCommand = new Command<TaskModel>(async (task) => await OnDeleteNote(task));
            UpdateTaskStatusCommand = new Command<TaskModel>(async (task) => await OnUpdateTaskStatus(task));
            LoadTasks();
        }

        public async void LoadTasks()
        {
            try
            {
                var taskList = await _connection.GetTasksAsync();
                Tasks = new System.Collections.ObjectModel.ObservableCollection<TaskModel>(taskList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading tasks: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load tasks: {ex.Message}", "OK");
            }
        }

        private async Task OnAddNote()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new NewTask());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to NewTask: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to open new task page: {ex.Message}", "OK");
            }
        }

        private async Task OnDeleteNote(TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    System.Diagnostics.Debug.WriteLine("DeleteNote: Task is null");
                    return;
                }
                await _connection.DeleteTaskAsync(task);
                Tasks.Remove(task);
                await Application.Current.MainPage.DisplayAlert("Success", "Task Removed!", "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting task: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to delete task: {ex.Message}", "OK");
            }
        }

        private async Task OnUpdateTaskStatus(TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    System.Diagnostics.Debug.WriteLine("UpdateTaskStatus: Task is null");
                    return;
                }
                System.Diagnostics.Debug.WriteLine($"Updating task status: Id={task.Id}, Name={task.TaskName}, Status={task.TaskStatus}");
                await _connection.SaveTaskAsync(task);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating task status: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to update task status: {ex.Message}", "OK");
            }
        }
    }
}