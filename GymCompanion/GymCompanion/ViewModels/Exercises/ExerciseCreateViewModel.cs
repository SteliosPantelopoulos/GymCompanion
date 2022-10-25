using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using System.Diagnostics;

namespace GymCompanion.ViewModels.Exercises
{
    [QueryProperty(nameof(ExerciseModel), "ExerciseModel")]
    public partial class ExerciseCreateViewModel : BaseViewModel
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

        public ExerciseCreateViewModel(ExerciseCalls exerciseCalls, BodyPartCalls bodyPartCalls)
        {
            this.Title = "Exercise details";
            this.exerciseCalls = exerciseCalls;
            this.bodyPartCalls = bodyPartCalls;
            Task.Run(async () => { this.BodyParts = await GetBodyPartsAsync(); }).Wait();
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
        async Task CreateExerciseAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CallsReturnModel<bool> model = await exerciseCalls.CreateExerciseAsync(_ExerciseModel.Name, _SelectedBodyPart.Id, _ExerciseModel.Description);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.Exercises.Create);

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
