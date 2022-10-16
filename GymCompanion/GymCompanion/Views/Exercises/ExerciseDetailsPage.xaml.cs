using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.ViewModels.Exercises;

namespace GymCompanion.Views.Exercises;

public partial class ExerciseDetailsPage : ContentPage
{
    private ExerciseDetailsViewModel _ViewModel;

    public ExerciseDetailsPage(ExerciseDetailsViewModel viewModel)
    {
        InitializeComponent();
        this._ViewModel = viewModel;
        BindingContext = _ViewModel;
    }

}