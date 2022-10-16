﻿using GymCompanion.Data.Models.Exercises;
using GymCompanion.Views.Exercises;

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
            try
            {
              await Shell.Current.GoToAsync($"{nameof(ExerciseCreatePage)}", true,
               new Dictionary<string, object>
               {
                    {nameof(ExerciseModel), new ExerciseModel() }
               });
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        [RelayCommand]
        async Task GetExercisesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                GetExercisesInfoModel model = await exercisesCall.GetExercisesInfoAsync();

                if (Exercises.Count != 0)
                    Exercises.Clear();

                foreach (ExerciseModel exercise in model.Exercises)
                    Exercises.Add(exercise);
            }
            catch (Exception exception)
            {
                /*Debug.WriteLine(exception);*/
                await Shell.Current.DisplayAlert("Error", "unable to get exercises", "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}