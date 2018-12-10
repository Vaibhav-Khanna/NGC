using Microsoft.WindowsAzure.MobileServices;
using NGC.DataStore.Abstraction.Stores;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();

      
        IContactCustomFieldSourceEntryStore ContactCustomFieldSourceEntryStore { get; }
        IContactCustomFieldSourceStore ContactCustomFieldSourceStore { get; }
        IContactCustomFieldStore ContactCustomFieldStore { get; }
        IContactStore ContactStore { get; }
        ICheckinStore CheckinStore { get; }
        ICheckinTypeStore CheckinTypeStore { get; }
        INoteStore NoteStore { get; }
        ICompanyStore CompanyStore { get; }
        IOpportunityStore OpportunityStore { get; }
        IContactSequenceStore ContactSequenceStore { get; } 
        IUserStore UserStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();

      
        Task<bool> LoginAsync(string username, string password);
        Task<bool> ForgotPasswordAsync(string email);
        Task VerifyTokenAsync();

        Task<bool> LogoutAsync();
    }
}
