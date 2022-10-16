using GymCompanion.ViewModels.Exercises;

namespace GymCompanion.Views.Exercises;

public partial class ExercisesList : ContentPage
{
	public ExercisesList(ExercisesListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}