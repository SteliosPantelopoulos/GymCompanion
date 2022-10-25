using GymCompanion.App.Calls;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.Exercises;
using GymCompanion.Data.Models.General;
using GymCompanion.Data.ServicesModels.BodyParts;
using GymCompanion.Data.ServicesModels.Exercises;
using GymCompanion.Data.ServicesModels.General;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace GymCompanion.Calls
{
    public class ExerciseCalls
    {
        public async Task<CallsReturnModel<GetExerciseInfoModel>> GetExerciseInfoAsync(string exerciseId)
        {
            CallsReturnModel<GetExerciseInfoModel> returnModel = new();
            string serviceUrl = "api/Exercises/GetExerciseInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"exerciseId", exerciseId }
            });

            HttpResponseMessage response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            if (response == null)
            {
                returnModel.StatusCode = HttpStatusCode.BadRequest;
                returnModel.Data = null;
            }
            else
            {
                returnModel.StatusCode = response.StatusCode;
                if (returnModel.StatusCode == HttpStatusCode.OK)
                    returnModel.Data = JsonConvert.DeserializeObject<GetExerciseInfoModel>(response.Content.ReadAsStringAsync().Result);
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<GetExercisesInfoModel>> GetExercisesInfoAsync()
        {
            CallsReturnModel<GetExercisesInfoModel> returnModel = new();
            string serviceUrl = "api/Exercises/GetExercisesInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> { });

            HttpResponseMessage response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            if (response == null)
            {
                returnModel.StatusCode = HttpStatusCode.BadRequest;
                returnModel.Data = null;
            }
            else
            {
                returnModel.StatusCode = response.StatusCode;
                if (returnModel.StatusCode == HttpStatusCode.OK)
                    returnModel.Data = JsonConvert.DeserializeObject<GetExercisesInfoModel>(response.Content.ReadAsStringAsync().Result);
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<bool>> CreateExerciseAsync(string name, int bodyPartId, string description)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/Exercises/CreateExercise?";

            CreateExerciseModel createExerciseModel = new CreateExerciseModel()
            {
                Name = name,
                BodyPartId = bodyPartId,
                Description = description
            };

            var json = JsonConvert.SerializeObject(createExerciseModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, data);

            if (response == null)
            {
                returnModel.StatusCode = HttpStatusCode.BadRequest;
                returnModel.Data = false;
            }
            else
            {
                returnModel.StatusCode = response.StatusCode;
                if (returnModel.StatusCode == HttpStatusCode.OK)
                    returnModel.Data = true;
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<bool>> DeleteExerciseAsync(int exerciseId)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/Exercises/DeleteExercise?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"exerciseId", exerciseId.ToString() }
                });

            HttpResponseMessage response = await ConnectionHelper.ContactWebServiceDeleteAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            if (response == null)
            {
                returnModel.StatusCode = HttpStatusCode.BadRequest;
                returnModel.Data = false;
            }
            else
            {
                returnModel.StatusCode = response.StatusCode;
                if (returnModel.StatusCode == HttpStatusCode.OK)
                    returnModel.Data = true;
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<bool>> UpdateExerciseAsync(int exerciseId, string name, int bodyPartId, string description)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/Exercises/UpdateExercise?";

            UpdateExerciseModel updateExerciseModel = new UpdateExerciseModel()
            {
                Id = exerciseId,
                Name = name,
                Description = description,
                BodyPartId = bodyPartId
            };
            var json = JsonConvert.SerializeObject(updateExerciseModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, data);

            if (response == null)
            {
                returnModel.StatusCode = HttpStatusCode.BadRequest;
                returnModel.Data = false;
            }
            else
            {
                returnModel.StatusCode = response.StatusCode;
                if (returnModel.StatusCode == HttpStatusCode.OK)
                    returnModel.Data = true;
            }

            return returnModel;
        }
    }
}
