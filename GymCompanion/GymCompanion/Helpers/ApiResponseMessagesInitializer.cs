using GymCompanion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GymCompanion.Helpers
{
    public static class ApiResponseMessagesInitializer
    {
        public static async Task TranslateStatusCodeToMessage(HttpStatusCode statusCode, ViewsNumerator.BodyParts view)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    if (view == ViewsNumerator.BodyParts.Create)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartCreateSuccess, Resources.Texts.ApplicationMessages.Ok);
                    else if (view == ViewsNumerator.BodyParts.Delete)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartDeleteSuccess, Resources.Texts.ApplicationMessages.Ok);
                    else if (view == ViewsNumerator.BodyParts.Update)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.BodyPartUpdateSuccess, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.Unauthorized:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.Unauthorized, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.InternalServerError:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.InternalServerError, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.BadRequest:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ApplicationError, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.NotFound:
                    if (view == ViewsNumerator.BodyParts.List)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.BodyPartsNotFound, Resources.Texts.ApplicationMessages.Ok);
                    else
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.BodyPartNotFound, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.Conflict:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.BodyPartExists, Resources.Texts.ApplicationMessages.Ok);
                    break;
            }
        }

        public static async Task TranslateStatusCodeToMessage(HttpStatusCode statusCode, ViewsNumerator.Exercises view)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    if (view == ViewsNumerator.Exercises.Create)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.ExerciseCreateSuccess, Resources.Texts.ApplicationMessages.Ok);
                    else if (view == ViewsNumerator.Exercises.Delete)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.ExerciseDeleteSuccess, Resources.Texts.ApplicationMessages.Ok);
                    else if (view == ViewsNumerator.Exercises.Update)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Success, Resources.Texts.ApplicationMessages.ExerciseUpdateSuccess, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.Unauthorized:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.Unauthorized, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.InternalServerError:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.InternalServerError, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.BadRequest:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ApplicationError, Resources.Texts.ApplicationMessages.Ok);
                    break;
                case HttpStatusCode.NotFound:
                    if (view == ViewsNumerator.Exercises.List)
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ExercisesNotFound, Resources.Texts.ApplicationMessages.Ok);
                    else
                    {
                        await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ExerciseNotFound, Resources.Texts.ApplicationMessages.Ok);
                        //TODO BodyPart not found
                    }
                    break;
                case HttpStatusCode.Conflict:
                    await Shell.Current.DisplayAlert(Resources.Texts.ApplicationMessages.Error, Resources.Texts.ApplicationMessages.ExerciseExists, Resources.Texts.ApplicationMessages.Ok);
                    break;
            }
        }




    }
}
