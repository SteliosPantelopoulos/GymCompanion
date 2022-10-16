using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using GymCompanion.Helpers;

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
                BooleanModel model = await bodyPartCalls.CreateBodyPartAsync(_BodyPartModel.Name);

                if (model.Result == true)
                {
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartCreateSuccess, Resources.Texts.ApplicationMessages.Ok);
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
                await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ApplicationError, Resources.Texts.ApplicationMessages.Ok);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
