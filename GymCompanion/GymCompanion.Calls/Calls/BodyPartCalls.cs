using GymCompanion.App.Calls;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.ServicesModels.BodyParts;
using GymCompanion.Data.ServicesModels.General;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GymCompanion.Calls
{
    public class BodyPartCalls
    {
        public async Task<CallsReturnModel<GetBodyPartInfoModel>> GetBodyPartInfoAsync(int bodyPartId)
        {
            CallsReturnModel<GetBodyPartInfoModel> returnModel = new();
            string serviceUrl = "api/BodyParts/GetBodyPartInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"bodyPartId", bodyPartId.ToString() }
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
                    returnModel.Data = JsonConvert.DeserializeObject<GetBodyPartInfoModel>(response.Content.ReadAsStringAsync().Result);
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<GetBodyPartsInfoModel>> GetBodyPartsInfoAsync()
        {
            CallsReturnModel<GetBodyPartsInfoModel> returnModel = new();

            string serviceUrl = "api/BodyParts/GetBodyPartsInfo?";

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
                    returnModel.Data = JsonConvert.DeserializeObject<GetBodyPartsInfoModel>(response.Content.ReadAsStringAsync().Result);
            }

            return returnModel;
        }

        public async Task<CallsReturnModel<bool>> CreateBodyPartAsync(string name)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/BodyParts/CreateBodyPart?";

            CreateBodyPartModel createBodyPartModel = new CreateBodyPartModel()
            {
                Name = name
            };
            var json = JsonConvert.SerializeObject(createBodyPartModel);
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

        public async Task<CallsReturnModel<bool>> DeleteBodyPartAsync(int bodyPartId)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/BodyParts/DeleteBodyPart?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"bodyPartId", bodyPartId.ToString() }
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

        public async Task<CallsReturnModel<bool>> UpdateBodyPartAsync(int bodyPartId, string name)
        {
            CallsReturnModel<bool> returnModel = new();
            string serviceUrl = "api/BodyParts/UpdateBodyPart?";

            UpdateBodyPartModel updateBodyPartModel = new UpdateBodyPartModel()
            {
                Id = bodyPartId,
                Name = name
            };
            var json = JsonConvert.SerializeObject(updateBodyPartModel);
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
