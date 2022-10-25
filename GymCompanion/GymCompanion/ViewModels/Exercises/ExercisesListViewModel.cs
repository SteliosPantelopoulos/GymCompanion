using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.General;
using GymCompanion.Helpers;
using GymCompanion.Views.Exercises;
using System.Diagnostics;
using System.Net;

namespace GymCompanion.ViewModels.Exercises
{
    public partial class ExercisesListViewModel : BaseViewModel
    {
        private readonly ExerciseCalls exercisesCall;

        public ObservableCollection<ExerciseModel> Exercises { get; set; } = new();
        public ExercisesListViewModel(ExerciseCalls exercisesCall)
        {
            Title = "Exercises Details";
            this.exercisesCall = exercisesCall;

        }

        [RelayCommand]
        async Task GoToDetailsAsync(ExerciseModel exercise)
        {
            if (exercise == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ExerciseDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {nameof(ExerciseModel), exercise }
                });
        }

        [RelayCommand]
        async Task GoToCreateAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(ExerciseCreatePage)}", true,
                 new Dictionary<string, object>
                 {
                    {nameof(ExerciseModel), new ExerciseModel() }
                 });
        }

        [RelayCommand]
        async Task GetExercisesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                if (Exercises.Count != 0)
                    Exercises.Clear();

                CallsReturnModel<GetExercisesInfoModel> model = await exercisesCall.GetExercisesInfoAsync();
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.Exercises.List);

                if (model.StatusCode == HttpStatusCode.OK)
                    foreach (ExerciseModel exercise in model.Data.Exercises)
                        Exercises.Add(exercise);
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
        async Task DeleteExerciseAsync(ExerciseModel exercise)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                CallsReturnModel<bool> model = await exercisesCall.DeleteExerciseAsync(exercise.Id);
                await ApiResponseMessagesInitializer.TranslateStatusCodeToMessage(model.StatusCode, ViewsNumerator.Exercises.Delete);
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
