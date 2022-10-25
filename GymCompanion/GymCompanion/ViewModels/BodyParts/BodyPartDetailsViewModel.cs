using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using System.Diagnostics;
using System.Net;

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
                CallsReturnModel<bool> model = await bodyPartCalls.UpdateBodyPartAsync(_BodyPartModel.Id, _BodyPartModel.Name);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.BodyParts.Update);
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
