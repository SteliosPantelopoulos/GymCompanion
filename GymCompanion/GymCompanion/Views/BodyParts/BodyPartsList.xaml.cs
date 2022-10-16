using GymCompanion.ViewModels.BodyParts;

namespace GymCompanion.Views.BodyParts;

public partial class BodyPartsList : ContentPage
{
	public BodyPartsList(BodyPartsListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}