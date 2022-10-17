using GymCompanion.App.Calls;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using Newtonsoft.Json;

namespace GymCompanion.Calls
{
    public class ExerciseCalls
    {
        public async Task<GetExerciseInfoModel> GetExerciseInfoAsync(string exerciseId)
        {
            string serviceUrl = "api/Exercises/GetExerciseInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"exerciseId", exerciseId }
            });

            var response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<GetExerciseInfoModel>(response);
        }

        public async Task<GetExercisesInfoModel> GetExercisesInfoAsync()
        {
            string serviceUrl = "api/Exercises/GetExercisesInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> { });

            string response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<GetExercisesInfoModel>(response);
        }

        public async Task<BooleanModel> CreateExerciseAsync(string name, int bodyPartId, string description)
        {
            string serviceUrl = "api/Exercises/CreateExercise?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"name", name },
                {"bodyPartId", bodyPartId.ToString() },
                {"description", description }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> DeleteExerciseAsync(int exerciseId)
        {
            string serviceUrl = "api/Exercises/DeleteExercise?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"exerciseId", exerciseId.ToString() }
            });

            string response = await ConnectionHelper.ContactWebServiceDeleteAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> UpdateExerciseAsync(int exerciseId, string newExerciseName, int newBodyPartId, string newDescription)
        {
            string serviceUrl = "api/Exercises/UpdateExercise?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"exerciseId", exerciseId.ToString() },
                {"newExerciseName", newExerciseName },
                {"newBodyPartId", newBodyPartId.ToString() },
                {"newDescription", newDescription }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }
    }
}
