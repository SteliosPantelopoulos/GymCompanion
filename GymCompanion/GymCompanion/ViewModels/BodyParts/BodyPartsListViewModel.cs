using GymCompanion.Data;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using GymCompanion.Helpers;
using GymCompanion.Views;
using GymCompanion.Views.BodyParts;

namespace GymCompanion.ViewModels.BodyParts
{
    public partial class BodyPartsListViewModel : BaseViewModel
    {
        BodyPartCalls bodyPartCalls;

        public ObservableCollection<BodyPartModel> BodyParts { get; set;} = new();


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
                GetBodyPartsInfoModel model = await bodyPartCalls.GetBodyPartsInfoAsync();

                if (BodyParts.Count != 0)
                    BodyParts.Clear();

                foreach (BodyPartModel bodyPart in model.BodyParts)
                    BodyParts.Add(bodyPart);
            }
            catch (Exception exception)
            {
                /*Debug.WriteLine(exception);*/
                await Shell.Current.DisplayAlert("Error", "unable to get bodyparts", "Ok");
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
                BooleanModel model = await bodyPartCalls.DeleteBodyPartAsync(bodyPart.Id);

                if (model.Result == true)
                {
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartDeleteSuccess, Resources.Texts.ApplicationMessages.Ok);
                }
                else
                {
                    if (model.ExceptionMessage != null)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.InternalServerError, Resources.Texts.ApplicationMessages.Ok);
                    else
                        await ApiResponseMessagesInitializer.ShowMessage(model.ApiResponseMessage);
                        switch (model.ApiResponseMessage)
                        {
                            case (int)Numerators.ApiResponseMessages.BodyPartNotFound:
                                await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.BodyPartNotFound, Resources.Texts.ApplicationMessages.Ok);
                                break;
                        }
                }
            }
            catch (Exception exception)
            {
                /*Debug.WriteLine(exception);*/
                await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ApplicationError, Resources.Texts.ApplicationMessages.Ok);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
