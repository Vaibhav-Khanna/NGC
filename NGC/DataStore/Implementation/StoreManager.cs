using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using NGC.DataStore.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NGC.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
       

        public async Task<object> LoginAsync(string email, string password)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "login", string.Empty));

            try
            {
                var credentials = new JObject();
                credentials["email"] = email;
                credentials["password"] = password;

                var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");

                var client = new HttpClient();

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<string>(content2);

                    // Store token in settings
                 
                    //
                    return resp;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public void LogoutAsync()
        {
           
        }

        public async Task<bool> RegenerateToken()
        {
            try
            {
                string Auth = string.Concat("token=","token");

                var uri = new Uri(string.Format(Constants.RestUrl + "regenerate" + "?" + Auth));

                var client = new HttpClient();

                var response = await client.GetAsync(uri);

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var resp = JsonConvert.DeserializeObject<string>(content);
                  
                    // Store token in settings
                  
                    //
                    return true;
                }
            }
            catch(Exception)
            {
                
            }

            return false;
        }

        public async Task<object> RegisterAsync(string email, string password,string deviceLanguage)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "register", string.Empty));

            var credentials = new JObject();
            credentials["email"] = email;
            credentials["password"] = password;
            credentials["language"] = deviceLanguage;

            var content = new StringContent(credentials.ToString(), Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<string>(responseContent);

                // Store token in settings
              
                //

                return tokenResponse;
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return "User already Exists";
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RequestNewPassword(string username,string password)
        {
            var uri = new Uri(string.Format(Constants.RestUrl + "forgot", string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(username);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

      

        //public async Task<byte[]> GetUserAvatar()
        //{
        //    string Auth = string.Concat("token=", Settings.Token);

        //    var uri = new Uri(string.Format(Constants.RestUrl + "users/me/avatar?" + Auth, string.Empty));

        //    var client = new HttpClient();

        //    var response = await client.GetAsync(uri);

        //    var content = await response.Content.ReadAsStreamAsync();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return ReadFully(content);
        //    }

        //    return null;
        //}


        //public static bool NeedsTokenRefresh()
        //{
        //    if (Settings.TokenExpiration == 0)
        //        return true;

        //    if (new DateTime(1970, 1, 1).AddSeconds(Settings.TokenExpiration).CompareTo(DateTime.Now.AddHours(2)) <= 0)
        //        return true;

        //    return false;
        //}

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
