using FreshMvvm;
using Microsoft.Identity.Client;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using NGC.DataStore.Abstraction;
using NGC.DataStore.Abstraction.Stores;
using NGC.DataStore.Implementation.Stores;
using NGC.Helpers;
using NGC.Models.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NGC.DataStore.Implementation
{
    public class StoreManager : IStoreManager
    {
        public bool IsInitialized { get; private set; }


        public static MobileServiceClient MobileService { get; set; }

    
        IContactCustomFieldSourceEntryStore contactCustomFieldSourceEntryStore;
        public IContactCustomFieldSourceEntryStore ContactCustomFieldSourceEntryStore => contactCustomFieldSourceEntryStore ?? (contactCustomFieldSourceEntryStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceEntryStore>());

        IContactCustomFieldSourceStore contactCustomFieldSourceStore;
        public IContactCustomFieldSourceStore ContactCustomFieldSourceStore => contactCustomFieldSourceStore ?? (contactCustomFieldSourceStore = FreshIOC.Container.Resolve<IContactCustomFieldSourceStore>());

        IContactCustomFieldStore contactCustomFieldStore;
        public IContactCustomFieldStore ContactCustomFieldStore => contactCustomFieldStore ?? (contactCustomFieldStore = FreshIOC.Container.Resolve<IContactCustomFieldStore>());

        IContactSequenceStore contactSequenceStore;
        public IContactSequenceStore ContactSequenceStore => contactSequenceStore ?? (contactSequenceStore = FreshIOC.Container.Resolve<IContactSequenceStore>());

        IContactStore contactStore;
        public IContactStore ContactStore => contactStore ?? (contactStore = FreshIOC.Container.Resolve<IContactStore>());
         
        IUserStore userStore;
        public IUserStore UserStore => userStore ?? (userStore = FreshIOC.Container.Resolve<IUserStore>());

        ICompanyStore companyStore;
        public ICompanyStore CompanyStore => companyStore ?? (companyStore = FreshIOC.Container.Resolve<ICompanyStore>());

        IOpportunityStore opportunityStore;
        public IOpportunityStore OpportunityStore => opportunityStore ?? (opportunityStore = FreshIOC.Container.Resolve<IOpportunityStore>());

        ICheckinStore checkinStore;
        public ICheckinStore CheckinStore => checkinStore ?? (checkinStore = FreshIOC.Container.Resolve<ICheckinStore>());

        ICheckinTypeStore checkinTypeStore;
        public ICheckinTypeStore CheckinTypeStore => checkinTypeStore ?? (checkinTypeStore = FreshIOC.Container.Resolve<ICheckinTypeStore>());

        INoteStore noteStore;
        public INoteStore NoteStore => noteStore ?? (noteStore = FreshIOC.Container.Resolve<INoteStore>());

        ITagStore tagStore;
        public ITagStore TagStore => tagStore ?? (tagStore = FreshIOC.Container.Resolve<ITagStore>());

        ICollectSourceStore collectSourceStore;
        public ICollectSourceStore CollectSourceStore => collectSourceStore ?? (collectSourceStore = FreshIOC.Container.Resolve<ICollectSourceStore>());

        ISalesTeamStore salesTeamStore;
        public ISalesTeamStore SalesTeamStore => salesTeamStore ?? (salesTeamStore = FreshIOC.Container.Resolve<ISalesTeamStore>());

        IUserSalesTeamStore userSalesTeamStore;
        public IUserSalesTeamStore UserSalesTeamStore => userSalesTeamStore ?? (userSalesTeamStore = FreshIOC.Container.Resolve<IUserSalesTeamStore>());



        public async Task<bool> LoginAsync(string username, string password)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }
            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var credentials = new JObject();
                credentials["email"] = username;
                credentials["password"] = password;
                credentials["mobile"] = true;

                var json_cred = credentials.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");
                var uriService = new Uri(Constants.LoginURL);

                var response = await _client.PostAsync(uriService, content);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();

                    var User = JsonConvert.DeserializeObject<Authentification>(content2);

                    MobileServiceUser user = new MobileServiceUser(User.UserId) { MobileServiceAuthenticationToken = User.Token };

                    MobileService.CurrentUser = user;

                    CacheToken(user, User);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
          

        }


        void CacheToken(MobileServiceUser user, Authentification UserProfile)
        {
            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = UserProfile.UserId;
            Settings.Role = UserProfile.Role.ToLower();
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            var mailObject = new JObject();
            mailObject["email"] = email;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var json_cred = mailObject.ToString();
                var content = new StringContent(json_cred, Encoding.UTF8, "application/json");
                var uriService = new Uri(Constants.ForgotURL);

                var response = await _client.PostAsync(uriService, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task VerifyTokenAsync()
        {

            if ((!string.IsNullOrEmpty(Settings.AuthToken)) && (!string.IsNullOrEmpty(Settings.UserId)))
            {
                try
                {
                    var date = JwtUtility.GetTokenExpiration(Settings.AuthToken).Value.AddMinutes(-30);

                    if (!string.IsNullOrEmpty(Settings.AuthToken) && date < DateTime.UtcNow)
                    {
                        var result = await RegenerateTokenAsync();

                        if (!result)
                        {
                            //no token regenerated
                            await LogoutAsync();
                        }
                    }
                }
                catch (InvalidTokenException)
                {
                    //Token exception error
                    await LogoutAsync();
                }
            }

        }

        private async Task<bool> RegenerateTokenAsync()
        {
            var uri = new Uri(Constants.RegenerateURL);

            var actualToken = Settings.AuthToken;

            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Add("token", actualToken);
                _client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content2 = await response.Content.ReadAsStringAsync();

                    var User = JsonConvert.DeserializeObject<Authentification>(content2);

                    MobileServiceUser user = new MobileServiceUser(User.UserId) { MobileServiceAuthenticationToken = User.Token };

                    MobileService.CurrentUser = user;

                    CacheToken(user, User);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }
           
            bool success = false;
           
            try
            {
                await MobileService.LogoutAsync();

                //Settings.AuthToken = string.Empty;
                //Settings.UserId = string.Empty;
                //Settings.Role = string.Empty;

                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

       
        IAccount GetUserByPolicy(IEnumerable<IAccount> users, string policy)
        {
            foreach (var user in users)
            {
                string userId = Base64UrlDecode(user.Username.Split('.')[0]);
                if (userId.EndsWith(policy.ToLower(), StringComparison.Ordinal))
                    return user;
            }
            return null;
        }

        string Base64UrlDecode(string str)
        {
            str = str.Replace('-', '+').Replace('_', '/');
            str = str.PadRight(str.Length + (4 - str.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(str);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public async Task DropEverythingAsync()
        {
            //Settings.UpdateDatabaseId();
            //TODO Do the update id for settings and add rest of the tables

          
            await ContactCustomFieldSourceEntryStore.DropTable();
            await ContactCustomFieldSourceStore.DropTable();
            await ContactCustomFieldStore.DropTable();
            await ContactStore.DropTable();
            await ContactSequenceStore.DropTable();
            await TagStore.DropTable();
            await CompanyStore.DropTable();
            await UserStore.DropTable();
            await CollectSourceStore.DropTable();
            await UserSalesTeamStore.DropTable();
            await SalesTeamStore.DropTable();
            await NoteStore.DropTable();
            await CheckinStore.DropTable();

            await OpportunityStore.DropTable();
           

            IsInitialized = false;

            Settings.UpdateDatabaseId();
        }


        object locker = new object();

        public async Task InitializeAsync()
        {
            MobileServiceSQLiteStore store;

            lock (locker)
            {

                if (IsInitialized)
                    return;

                IsInitialized = true;

                MobileService = new MobileServiceClient(Constants.ApplicationURL);

                var dbId = Settings.DatabaseId;

                string path = "";

                if (dbId == 0)
                    path = $"syncstore.db";
                else
                    path = $"syncstore{dbId}.db";

                
                store = new MobileServiceSQLiteStore(path);

               
                store.DefineTable<ContactCustomField>();
                store.DefineTable<ContactCustomFieldSource>();
                store.DefineTable<ContactCustomFieldSourceEntry>();
                store.DefineTable<Contact>();
                store.DefineTable<Company>();
                store.DefineTable<Checkin>();
                store.DefineTable<CollectSource>();
                store.DefineTable<SalesTeam>();
                store.DefineTable<UserSalesTeam>();
                store.DefineTable<Tag>();
                store.DefineTable<Note>();
                store.DefineTable<CheckinType>();
                store.DefineTable<Opportunity>();
                store.DefineTable<ContactSequence>();
               
                store.DefineTable<User>();

                //TODO Add rest of the tables
            }

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()).ConfigureAwait(false);

            LoadCachedTokenAsync();
        }

         void LoadCachedTokenAsync()
        {
                try
                {
                    if (!string.IsNullOrEmpty(Settings.AuthToken) && JwtUtility.GetTokenExpiration(Settings.AuthToken) > DateTime.UtcNow)
                    { 
                        MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                        MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
                    }
                }
                catch (InvalidTokenException)
                {
                    Settings.AuthToken = string.Empty;
                    Settings.UserId = string.Empty;
                    Settings.Role = string.Empty;

                }
        }

        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if (!IsInitialized)
                await InitializeAsync();

    here:   var taskList = new List<Task<bool>>();
          
            taskList.Add(ContactCustomFieldSourceEntryStore.SyncAsync());
            taskList.Add(ContactCustomFieldSourceStore.SyncAsync());
            taskList.Add(ContactCustomFieldStore.SyncAsync());
            taskList.Add(ContactStore.SyncAsync());
            taskList.Add(OpportunityStore.SyncAsync());
            taskList.Add(CompanyStore.SyncAsync());
            taskList.Add(CheckinTypeStore.SyncAsync());
            taskList.Add(NoteStore.SyncAsync());
            taskList.Add(CollectSourceStore.SyncAsync());
            taskList.Add(UserSalesTeamStore.SyncAsync());
            taskList.Add(SalesTeamStore.SyncAsync());
            taskList.Add(TagStore.SyncAsync());
            taskList.Add(CheckinStore.SyncAsync());
            taskList.Add(ContactSequenceStore.SyncAsync());     
            taskList.Add(UserStore.SyncAsync());

            bool[] successes;

            successes = await Task.WhenAll(taskList);

            if (successes.Any(x => x == false))
                goto here;

            if (syncUserSpecific)
            {
                // add stores that are user specific data                       
            }

            return successes.Any(x => !x); //if any were a failure.
        }

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