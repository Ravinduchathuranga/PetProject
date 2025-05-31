using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;

namespace PetProject.ViewModels
{
    public partial class NewTaskViewModel : BaseViewModel
    {
        private readonly Connection _connection;
        private string _noteText = string.Empty; // Initialize to avoid CS8618
        private bool _isErrorVisible;
        private string _errorText = string.Empty; // Initialize to avoid CS8618

        public string NoteText
        {
            get => _noteText;
            set => SetProperty(ref _noteText, value);
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set => SetProperty(ref _isErrorVisible, value);
        }

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }

        public ICommand AddTaskCommand { get; }

        public NewTaskViewModel()
        {
            _connection = new Connection();
            NoteText = string.Empty;
            IsErrorVisible = false;
            ErrorText = "Task can not be empty";
            AddTaskCommand = new Command(async () => await OnAddTask());
        }

        private async Task OnAddTask()
        {
            if (string.IsNullOrWhiteSpace(NoteText))
            {
                IsErrorVisible = true;
                ErrorText = "Please enter a topic.";
                return;
            }
            else if (NoteText.Length >= 20)
            {
                IsErrorVisible = true;
                ErrorText = "Please enter less than 20 characters topic.";
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"Attempting to save task: {NoteText}");
                var task = new TaskModel
                {
                    TaskName = NoteText,
                    TaskStatus = false
                };
                await _connection.SaveTaskAsync(task);
                await Application.Current.MainPage.DisplayAlert("Success", "Note added successfully!", "OK");
                NoteText = string.Empty;
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (NotImplementedException ex)
            {
                System.Diagnostics.Debug.WriteLine($"NotImplementedException in OnAddTask: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Warning", "Adding tasks is not yet fully implemented.", "OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in OnAddTask: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to add task: {ex.Message}", "OK");
            }
        }
    }
}