using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Helpers;

namespace GymCompanion.Views.Shared.Exercises;

public partial class ExercisesFormPartialPage : ContentPage
{

    private int ViewCallFrom;
    bool nameEntryCompleted = false, bodyPartPickerCompleted = false, descriptionEntryCompleted = false;

    public ExercisesFormPartialPage()
    {

    }

    public ExercisesFormPartialPage(int caller)
    {
        InitializeComponent();

        if (caller == (int)ViewsNumerator.Exercises.Create)
        {
            ViewCallFrom = (int)ViewsNumerator.Exercises.Create;
            CreateButton.IsVisible = true;
            UpdateButton.IsVisible = false;
        }
        else if (caller == (int)ViewsNumerator.Exercises.Details)
        {
            ViewCallFrom = (int)ViewsNumerator.Exercises.Details;
            CreateButton.IsVisible = false;
            UpdateButton.IsVisible = true;
        }

    }

    private void ValidateNameEntry(object sender, TextChangedEventArgs e)
    {
        Entry NameEntry = (Entry)sender;
        if (String.IsNullOrEmpty(e.NewTextValue))
        {
            NameEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationFalse");
            nameEntryCompleted = false;
        }
        else
        {
            NameEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationTrue");
            nameEntryCompleted = true;
        }

        SetSubmitButtonStyle();
    }
    private void ValidateBodyPartPicker(object sender, EventArgs e)
    {
        Picker BodyPartPicker = (Picker)sender;

        BodyPartPicker.Style = (Style)ResourcesHelper.FindResource("PickerValidationTrue");
        bodyPartPickerCompleted = true;

        SetSubmitButtonStyle();
    }

    private void ValidateDescriptionEntry(object sender, TextChangedEventArgs e)
    {
        Entry DescriptionEntry = (Entry)sender;

        if (String.IsNullOrEmpty(e.NewTextValue))
        {
            DescriptionEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationFalse");
            descriptionEntryCompleted = false;
        }
        else
        {
            DescriptionEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationTrue");
            descriptionEntryCompleted = true;
        }

        SetSubmitButtonStyle();
    }



    private void SetSubmitButtonStyle()
    {
        if(ViewCallFrom == (int)ViewsNumerator.Exercises.Create)
        {
            if (nameEntryCompleted && bodyPartPickerCompleted && descriptionEntryCompleted)
                CreateButton.Style = (Style)ResourcesHelper.FindResource("ButtonEnabled");
            else
                CreateButton.Style = (Style)ResourcesHelper.FindResource("ButtonDisabled");
        }
        else if(ViewCallFrom == (int)ViewsNumerator.Exercises.Details)
        {
            if (nameEntryCompleted && bodyPartPickerCompleted && descriptionEntryCompleted)
                UpdateButton.Style = (Style)ResourcesHelper.FindResource("ButtonEnabled");
            else
                UpdateButton.Style = (Style)ResourcesHelper.FindResource("ButtonDisabled");
        }
        
    }





    //private void ValidateNameEntry(object sender, TextChangedEventArgs e)
    //{
    //    Entry NameEntry = (Entry)sender;
    //    if (String.IsNullOrEmpty(e.NewTextValue))
    //    {
    //        NameEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationFalse");
    //        nameEntryCompleted = false;
    //    }
    //    else
    //    {
    //        NameEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationTrue");
    //        nameEntryCompleted = true;
    //    }

    //    SetSubmitButtonStyle();
    //}
    //private void ValidateBodyPartPicker(object sender, EventArgs e)
    //{
    //    Picker BodyPartPicker = (Picker)sender;

    //    BodyPartPicker.Style = (Style)ResourcesHelper.FindResource("PickerValidationTrue");
    //    bodyPartPickerCompleted = true;

    //    SetSubmitButtonStyle();
    //}

    //private void ValidateDescriptionEntry(object sender, TextChangedEventArgs e)
    //{
    //    Entry DescriptionEntry = (Entry)sender;

    //    if (String.IsNullOrEmpty(e.NewTextValue))
    //    {
    //        DescriptionEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationFalse");
    //        descriptionEntryCompleted = false;
    //    }
    //    else
    //    {
    //        DescriptionEntry.Style = (Style)ResourcesHelper.FindResource("EntryValidationTrue");
    //        descriptionEntryCompleted = true;
    //    }

    //    SetSubmitButtonStyle();
    //}



    //private void SetSubmitButtonStyle()
    //{
    //    if (nameEntryCompleted && bodyPartPickerCompleted && descriptionEntryCompleted)
    //        SubmitButton.Style = (Style)ResourcesHelper.FindResource("ButtonEnabled");
    //    else
    //        SubmitButton.Style = (Style)ResourcesHelper.FindResource("ButtonDisabled");
    //}
}