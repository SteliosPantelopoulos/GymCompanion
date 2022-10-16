using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using GymCompanion.Helpers;
using System.Diagnostics;

namespace GymCompanion.ViewModels.BodyParts
{
    [QueryProperty(nameof(BodyPartModel), "BodyPartModel")]
    public partial class BodyPartDetailsViewModel : BaseViewModel
    {
        BodyPartCalls bodyPartCalls;

        [ObservableProperty]
        BodyPartModel _BodyPartModel;

        public BodyPartDetailsViewModel(BodyPartCalls bodyPartCalls)
        {
            this.Title = Resources.Texts.BodyParts.Details;
            this.bodyPartCalls = bodyPartCalls;
        }

        [RelayCommand]
        async Task UpdateBodyPartAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                BooleanModel model = await bodyPartCalls.UpdateBodyPartAsync(_BodyPartModel.Id, _BodyPartModel.Name);

                if (model.Result == true)
                {
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartUpdateSuccess, Resources.Texts.ApplicationMessages.Ok);
                }
                else
                {
                    if (model.ExceptionMessage != null)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.InternalServerError, Resources.Texts.ApplicationMessages.Ok);
                    else
                        await ApiResponseMessagesInitializer.ShowMessage(model.ApiResponseMessage);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ApplicationError, Resources.Texts.ApplicationMessages.Ok);
            }
            finally
            {
                IsBusy = false;
            }
        }

        


    }
}
