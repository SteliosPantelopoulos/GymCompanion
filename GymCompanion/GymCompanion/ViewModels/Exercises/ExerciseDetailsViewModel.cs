using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using GymCompanion.Helpers;
using System.Diagnostics;

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
            GetBodyPartsInfoModel bodyPartsInfoModel = await bodyPartCalls.GetBodyPartsInfoAsync();
            _BodyParts = bodyPartsInfoModel.BodyParts;

            return bodyPartsInfoModel.BodyParts;
        }

        [RelayCommand]
        public async Task<BodyPartModel> GetSelectedBodyPartAsync()
        {
            GetBodyPartInfoModel bodyPartInfoModel = await bodyPartCalls.GetBodyPartInfoAsync(_ExerciseModel.BodyPartId);
            _SelectedBodyPart = bodyPartInfoModel.BodyPart;

            return bodyPartInfoModel.BodyPart;
        }

        [RelayCommand]
        async Task UpdateExerciseAsync()
        {
            if(SelectedBodyPart == null)
                Task.Run(async () => { this.SelectedBodyPart = await GetSelectedBodyPartAsync(); }).Wait();

            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                BooleanModel model = await exerciseCalls.UpdateExerciseAsync(_ExerciseModel.Id, _ExerciseModel.Name, _SelectedBodyPart.Id, _ExerciseModel.Description);

                if (model.Result == true)
                {
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.ExerciseUpdateSuccess, Resources.Texts.ApplicationMessages.Ok);
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
