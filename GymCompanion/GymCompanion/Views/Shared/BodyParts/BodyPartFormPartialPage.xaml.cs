using GymCompanion.Data.Models.BodyParts;

namespace GymCompanion.Views.Shared.BodyParts;

public partial class BodyPartFormPartialPage : ContentView
{
    public BodyPartFormPartialPage()
    {

    }

    public BodyPartFormPartialPage(int caller)
	{
        InitializeComponent();
        //todo changed
        if (caller == (int)ViewsNumerator.BodyParts.Create)
        {
            CreateButton.IsVisible = true;
            UpdateButton.IsVisible = false;
        }
        else if (caller == (int)ViewsNumerator.BodyParts.Details)
        {
            CreateButton.IsVisible = false;
            UpdateButton.IsVisible = true;
        }
    }
}