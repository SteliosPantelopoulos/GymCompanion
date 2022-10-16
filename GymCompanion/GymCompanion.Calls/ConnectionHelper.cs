using GymCompanion.Calls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7080/api/");

                var context = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)

                });

                var response = client.PostAsync("Token", context);

                var jsonResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Result.Content.ReadAsStringAsync().Result);

                return jsonResult["access_token"];

            }
        }

        public static async Task<string> ContactWebServiceGetAsync(string serviceUrl, string parameters = "")
        {
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", System.Web.HttpContext.Current.Session["WebServiceToken"].ToString());*/
            /*client.BaseAddress = new Uri("http://localhost:7080/api/");*/
            try
            {
                var response = await client.GetAsync(Base + serviceUrl + parameters);
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
            

            
        }

        public static async Task<string> ContactWebServicePostAsync(string serviceUrl, string parameters = "")
        {
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", System.Web.HttpContext.Current.Session["WebServiceToken"].ToString());*/
            /*client.BaseAddress = new Uri("https://localhost:7080/api/");*/

            var response = await client.PostAsync(Base + serviceUrl + parameters, null);

            return response.Content.ReadAsStringAsync().Result;
        }

        public static async Task<string> ContactWebServiceDeleteAsync(string serviceUrl, string parameters = "")
        {
            /*client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", System.Web.HttpContext.Current.Session["WebServiceToken"].ToString());*/
            /*client.BaseAddress = new Uri("https://localhost:7080/api/");*/

            var response = await client.DeleteAsync(Base + serviceUrl + parameters);

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
