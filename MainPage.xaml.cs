namespace PetProject
{
    public partial class MainPage : ContentPage
    {
        private readonly Connection connection;
        public MainPage()
        {
            InitializeComponent();
            connection = new Connection();

            Task.Run(async () => taskListView.ItemsSource = await connection.GetTasksAsync());
        }

        private void OnAddNoteClicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTask());
        }

        private async void OnDeleteNoteClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.CommandParameter is TaskModel task)
            {
                try
                {
                    await connection.DeleteTaskAsync(task);
                    taskListView.ItemsSource = await connection.GetTasksAsync(); // Refresh list
                    await DisplayAlert("Success", "Task Removed!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to delete task: {ex.Message}", "OK");
                }
            }
        }

    }
}