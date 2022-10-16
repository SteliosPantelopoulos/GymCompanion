using GymCompanion.ViewModels.BodyParts;

namespace GymCompanion.Views.BodyParts;

public partial class BodyPartDetailsPage : ContentPage
{
	private BodyPartDetailsViewModel _ViewModel;
	public BodyPartDetailsPage(BodyPartDetailsViewModel viewModel)
	{
		InitializeComponent();
		this._ViewModel = viewModel;
		BindingContext = _ViewModel;
	}

}