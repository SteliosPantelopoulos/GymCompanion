using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using System.Diagnostics;
using System.Net;

namespace GymCompanion.ViewModels.Exercises
{
    [QueryProperty(nameof(ExerciseModel), "ExerciseModel")]
    public partial class ExerciseDetailsViewModel : BaseViewModel
    {
        ExerciseCalls exerciseCalls;
        BodyPartCalls bodyPartCalls;

        [ObservableProperty]
        ExerciseModel _ExerciseModel;

        [ObservableProperty]
        List<BodyPartModel> _BodyParts = new();

        [ObservableProperty]
        BodyPartModel _SelectedBodyPart;

        [ObservableProperty]
        string _SelectedBodyPartName;

        public ExerciseDetailsViewModel(ExerciseCalls exerciseCalls, BodyPartCalls bodyPartCalls)
        {
            this.Title = "Exercise details";
            this.exerciseCalls = exerciseCalls;
            this.bodyPartCalls = bodyPartCalls;
            Task.Run(async () => { this.BodyParts = await GetBodyPartsAsync(); }).Wait();
            //TODO Need to show the selected body part id
        }

        [RelayCommand]
        public async Task<List<BodyPartModel>> GetBodyPartsAsync()
        {
            CallsReturnModel<GetBodyPartsInfoModel> bodyPartsInfoModel = await bodyPartCalls.GetBodyPartsInfoAsync();

            if (bodyPartsInfoModel.Data != null)
                _BodyParts = bodyPartsInfoModel.Data.BodyParts;

            return _BodyParts;
        }

        [RelayCommand]
        public async Task<BodyPartModel> GetSelectedBodyPartAsync()
        {
            //TODO CHECK THE NULL RETURN
            CallsReturnModel<GetBodyPartInfoModel> model = await bodyPartCalls.GetBodyPartInfoAsync(_ExerciseModel.BodyPartId);
            await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.BodyParts.Details);

            if (model.StatusCode == HttpStatusCode.OK)
            {
                _SelectedBodyPart = model.Data.BodyPart;
                return model.Data.BodyPart;
            }
            else
            {
                _SelectedBodyPart = null;
                return null;
            }
        }

        [RelayCommand]
        async Task UpdateExerciseAsync()
        {
            if (SelectedBodyPart == null)
                Task.Run(async () => { this.SelectedBodyPart = await GetSelectedBodyPartAsync(); }).Wait();

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CallsReturnModel<bool> model = await exerciseCalls.UpdateExerciseAsync(_ExerciseModel.Id, _ExerciseModel.Name, _SelectedBodyPart.Id, _ExerciseModel.Description);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.Exercises.Update);

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
