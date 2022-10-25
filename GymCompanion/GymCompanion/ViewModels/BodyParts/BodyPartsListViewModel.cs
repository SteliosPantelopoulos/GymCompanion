using GymCompanion.Data;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using GymCompanion.Views;
using GymCompanion.Views.BodyParts;
using System.Diagnostics;
using System.Net;

namespace GymCompanion.ViewModels.BodyParts
{
    public partial class BodyPartsListViewModel : BaseViewModel
    {
        BodyPartCalls bodyPartCalls;

        public ObservableCollection<BodyPartModel> BodyParts { get; set; } = new();

        public BodyPartsListViewModel(BodyPartCalls bodyPartsCalls)
        {
            Title = "BodyParts Details";

            this.bodyPartCalls = bodyPartsCalls;
        }

        [RelayCommand]
        async Task GetBodyPartsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (BodyParts.Count != 0)
                    BodyParts.Clear();

                CallsReturnModel<GetBodyPartsInfoModel> model = await bodyPartCalls.GetBodyPartsInfoAsync();
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.BodyParts.List);

                if (model.StatusCode == HttpStatusCode.OK)
                    foreach (BodyPartModel bodyPart in model.Data.BodyParts)
                        BodyParts.Add(bodyPart);
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

        [RelayCommand]
        async Task GoToDetailsAsync(BodyPartModel bodyPart)
        {
            if (bodyPart == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(BodyPartDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {nameof(BodyPartModel), bodyPart }
                });
        }

        [RelayCommand]
        async Task GoToCreateAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(BodyPartCreatePage)}", true,
                new Dictionary<string, object>
                {
                    {nameof(BodyPartModel), new BodyPartModel() }
                });
        }

        [RelayCommand]
        async Task DeleteBodyPartAsync(BodyPartModel bodyPart)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CallsReturnModel<bool> model = await bodyPartCalls.DeleteBodyPartAsync(bodyPart.Id);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.BodyParts.Delete);
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
