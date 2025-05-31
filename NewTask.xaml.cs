
using System.Threading.Tasks;

namespace PetProject;

public partial class NewTask : ContentPage
{
    private readonly Connection connection;
    public NewTask()
    {
        InitializeComponent();
        ErrorLabel.IsVisible = false;
    }
    private async void OnAddTask(Object sender, EventArgs e)
    {
        String noteText = AddNote.Text;

        if (String.IsNullOrWhiteSpace(noteText))
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
            await connection.SaveTaskAsync(new TaskModel

            {
                
                TaskName = noteText,
                TaskStatus=false
            });

            _=DisplayAlert("Success", "Note added successfully!", "OK");

            AddNote.Text = String.Empty;

            _=Navigation.PopAsync();
        }
    }
}