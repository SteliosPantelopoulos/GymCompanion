using GymCompanion.App.Calls;
using GymCompanion.Data.Models.Account;
using GymCompanion.Data.Models.General;
using Newtonsoft.Json;

namespace GymCompanion.Calls
{
    public class AccountCalls
    {
        public async Task<BooleanModel> LoginAsync(string username, string password)
        {
            string serviceUrl = "api/Account/Login?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username },
                {"password", password }
            });

            string response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> RegisterAsync(string username, string password, string email, string firstname, string lastname, string country, string birthday, string registrationDate)
        {
            string serviceUrl = "api/Account/Register?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username },
                {"password", password },
                {"email", email },
                {"firstname", firstname },
                {"lastname", lastname },
                {"country", country },
                {"birthday", birthday },
                {"registrationDate", registrationDate }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<GetUserInfoModel> GetAccountInfoAsync(string username)
        {
            string serviceUrl = "api/Account/GetAccountInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username }
            });

            string response = await ConnectionHelper.ContactWebServiceGetAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<GetUserInfoModel>(response);
        }

        public async Task<BooleanModel> UpdateAccountInfoAsync(string username, string newEmail, string newFirstname, string newLastname, string newCountry, string newBirthday)
        {
            string serviceUrl = "api/Account/UpdateAccountInfo?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username },
                {"newEmail", newEmail },
                {"newFirstname", newFirstname },
                {"newLastname", newLastname },
                {"newCountry", newCountry },
                {"newBirthday", newBirthday }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> DeleteAccountAsync(string username)
        {
            string serviceUrl = "api/Account/DeleteAccount?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", username }
            });

            string response = await ConnectionHelper.ContactWebServiceDeleteAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> ChangeUsernameAsync(string oldUsername, string newUsername)
        {
            string serviceUrl = "api/Account/ChangeUsername?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"newUsername", newUsername },
                {"oldUsername", oldUsername }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }

        public async Task<BooleanModel> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            string serviceUrl = "api/Account/ChangePassword?";

            var parameters = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"newPassword", newPassword },
                {"oldPassword", oldPassword }
            });

            string response = await ConnectionHelper.ContactWebServicePostAsync(serviceUrl, parameters.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<BooleanModel>(response);
        }
    }
}
