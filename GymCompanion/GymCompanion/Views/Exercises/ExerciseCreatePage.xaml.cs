using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.ViewModels.Exercises;

namespace GymCompanion.Views.Exercises;

public partial class ExerciseCreatePage : ContentPage
{
    private ExerciseCreateViewModel _ViewModel;

    public ExerciseCreatePage(ExerciseCreateViewModel viewModel)
    {
        InitializeComponent();
        this._ViewModel = viewModel;
        BindingContext = _ViewModel;
    }

}