namespace PetProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddNoteClicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewTask());
        }
    }
}