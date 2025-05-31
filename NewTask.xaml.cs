
namespace PetProject;

public partial class NewTask : ContentPage
{
    public NewTask()
    {
        InitializeComponent();
        ErrorLabel.IsVisible = false;
    }
    private void OnAddTask(Object sender, EventArgs e)
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
            
            DisplayAlert("Success", "Note added successfully!", "OK");

            AddNote.Text = String.Empty;

            Navigation.PopAsync();
        }
    }
}