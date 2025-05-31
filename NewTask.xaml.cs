using System.Threading.Tasks;

namespace PetProject;

public partial class NewTask : ContentPage
{
    public NewTask() { 
        InitializeComponent();
        BindingContext = new ViewModels.NewTaskViewModel();
    } 
}