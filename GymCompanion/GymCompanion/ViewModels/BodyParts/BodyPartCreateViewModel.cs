using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using System.Diagnostics;

namespace GymCompanion.ViewModels.BodyParts
{
    [QueryProperty(nameof(BodyPartModel), "BodyPartModel")]
    public partial class BodyPartCreateViewModel : BaseViewModel
    {
        BodyPartCalls bodyPartCalls;

        [ObservableProperty]
        BodyPartModel _BodyPartModel;

        public BodyPartCreateViewModel(BodyPartCalls bodyPartCalls)
        {
            this.Title = "Create body part";
            this.bodyPartCalls = bodyPartCalls;
        }

        [RelayCommand]
        async Task CreateBodyPartAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CallsReturnModel<bool> model = await bodyPartCalls.CreateBodyPartAsync(_BodyPartModel.Name);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.BodyParts.Create);
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
