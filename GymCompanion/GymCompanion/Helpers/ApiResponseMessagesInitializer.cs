using GymCompanion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymCompanion.Helpers
{
    public static class ApiResponseMessagesInitializer
    {
        public static async Task ShowMessage(int apiResponseMessageCode)
        {
            switch (apiResponseMessageCode)
            {
                case (int)Numerators.ApiResponseMessages.EmailIsUsed:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.EmailIsUsed, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.UsernameIsUsed:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.UsernameIsUsed, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.ExerciseNameIsUsed:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.ExerciseNameIsUsed, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.BodyPartNameIsUsed:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.BodyPartNameIsUsed, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.ExerciseNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.ExerciseNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.ExercisesNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.ExercisesNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.UserNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.UserNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.BodyPartNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.BodyPartNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.BodyPartsNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.BodyPartsNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.WorkoutNotFound:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.WorkoutNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case (int)Numerators.ApiResponseMessages.WrongCredentials:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Failure, Resources.Texts.ApplicationMessages.WrongCredentials, Resources.Texts.ApplicationMessages.Ok);
                    break;

            }
        }
    }
}
