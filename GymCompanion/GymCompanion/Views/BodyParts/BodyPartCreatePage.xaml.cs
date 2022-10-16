using GymCompanion.ViewModels.BodyParts;

namespace GymCompanion.Views.BodyParts;

public partial class BodyPartCreatePage : ContentPage
{
    private BodyPartCreateViewModel _ViewModel;
    public BodyPartCreatePage(BodyPartCreateViewModel viewModel)
    {
        InitializeComponent();
        this._ViewModel = viewModel;
        BindingContext = _ViewModel;
    }

}