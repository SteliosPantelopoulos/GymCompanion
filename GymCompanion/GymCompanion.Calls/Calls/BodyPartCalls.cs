using GymCompanion.App.Calls;
using GymCompanion.Data.Models.BodyParts;
using GymCompanion.Data.Models.General;
using Newtonsoft.Json;

namespace GymCompanion.Calls
{
    public class BodyPartCalls
    {
        public async Task<GetBodyPartInfoModel> GetBodyPartInfoAsync(int bodyPartId)
        {
            string serviceUrl = "api/BodyParts/GetBodyPartInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"bodyPartId", bodyPartId.ToString() }
            });

            string response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<GetBodyPartInfoModel>(response);
        }

        public async Task<GetBodyPartsInfoModel> GetBodyPartsInfoAsync()
        {
            string serviceUrl = "api/BodyParts/GetBodyPartsInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string> { });

            string response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<GetBodyPartsInfoModel>(response);
        }

        public async Task<BooleanModel> CreateBodyPartAsync(string name)
        {
            string serviceUrl = "api/BodyParts/CreateBodyPart?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"name", name }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> DeleteBodyPartAsync(int bodyPartId)
        {
            string serviceUrl = "api/BodyParts/DeleteBodyPart?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"bodyPartId", bodyPartId.ToString() }
            });

            string response = await ConnectionHelper.ContactWebServiceDeleteAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> UpdateBodyPartAsync(int bodyPartId, string newName)
        {
            string serviceUrl = "api/BodyParts/UpdateBodyPart?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"bodyPartId", bodyPartId.ToString() },
                {"newName", newName }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }
    }
}
