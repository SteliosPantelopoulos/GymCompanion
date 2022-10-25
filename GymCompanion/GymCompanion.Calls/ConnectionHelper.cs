using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Security.Principal;

namespace GymCompanion.App.Calls
{

    public class ConnectionHelper
    {
        #if DEBUG
                private static readonly string Base = "http://localhost:7080/";
        #else
                 private static readonly string Base = "https://localhost:7081/";
        #endif

        private static HttpClient client = new HttpClient();


        private static async Task<string> GetToken(string username, string password)
        {
            string serviceUrl = String.Format("Account/GetToken?username={0}&password={1}", username, password);
            try
            {
                var response = await client.GetAsync("http://localhost:7080/api/Account/GetToken?username=stpant&password=P@ssw0rd");

                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception exception )
            {
                return "";
            }
            //using (var client = new HttpClient())
            //{

            //    var context = new FormUrlEncodedContent(new[]
            //    {
            //        //new KeyValuePair<string, string>("grant_type", "password"),
            //        new KeyValuePair<string, string>("username", username),
            //        new KeyValuePair<string, string>("password", password)

            //    });

            //    var response = await client.GetAsync(Base + "Account/GetToken?username=stpant&password=P@ssw0rd");

            //    var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Result.Content.ReadAsStringAsync().Result);

            //    return jsonResult["access_token"];
            //}
        }

        public static async Task<HttpResponseMessage> ContactWebServiceGetAsync(string serviceUrl, string parameters = "")
        {
            try
            {
                dynamic data = JObject.Parse((await GetToken("stpant", "P@ssw0rd")).ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data.token.ToString());
                var response = await client.GetAsync(Base + serviceUrl + parameters);

                return response;
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public static async Task<HttpResponseMessage> ContactWebServicePostAsync(string serviceUrl, HttpContent content = null)
        {
            try
            {
                dynamic data = JObject.Parse((await GetToken("stpant", "P@ssw0rd")).ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data.token.ToString());
                var response = await client.PostAsync(Base + serviceUrl, content);
                return response;
            }
            catch (Exception exception)
            {
                return null;
            } 
        }

        public static async Task<HttpResponseMessage> ContactWebServiceDeleteAsync(string serviceUrl, string parameters = "")
        {
            try
            {
                dynamic data = JObject.Parse((await GetToken("stpant", "P@ssw0rd")).ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", data.token.ToString());
                var response = await client.DeleteAsync(Base + serviceUrl + parameters);
                return response;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
