using System.Threading.Tasks;

namespace PetProject;

public partial class NewTask : ContentPage
{
    private readonly Connection connection;

    public NewTask()
    {
        InitializeComponent();
        if (AddNote == null) DisplayAlert("Error", "AddNote is null", "OK");
        connection = new Connection(); // Ensure 'connection' is initialized here
        if (connection == null) DisplayAlert("Error", "Connection is null", "OK");
        ErrorLabel.IsVisible = false;
    }

    private async void OnAddTask(object sender, EventArgs e)
    {
        if (AddNote == null)
        {
            await DisplayAlert("Error", "AddNote is null", "OK");
            return;
        }

        string noteText = AddNote.Text;

        if (string.IsNullOrWhiteSpace(noteText))
        {
            ErrorLabel.IsVisible = true;
            ErrorLabel.Text = "Please enter a topic.";
        }
        else if (noteText.Length >= 20)
        {
            ErrorLabel.IsVisible = true;
            ErrorLabel.Text = "Please enter less than 20 characters topic.";
        }
        else
        {
            try
            {
                await connection.SaveTaskAsync(new TaskModel
                {
                    TaskName = noteText,
                    TaskStatus = false
                });
                await DisplayAlert("Success", "Note added successfully!", "OK");
                AddNote.Text = string.Empty;
                if (Navigation == null)
                {
                    await DisplayAlert("Error", "Navigation is null", "OK");
                    return;
                }
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to add task: {ex.Message}", "OK");
            }
        }
    }
}