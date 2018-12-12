using NGC.DataModels;
using NGC.DataStore.Implementation.Stores;
using NGC.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGC.DataStore.Abstraction.Stores
{
    public interface IContactStore : IBaseStore<Contact>
    {
        // Task<IEnumerable<Contact>> GetItemsByFilterAsync(string Filter, string SortName, bool SortValue, bool forceRefresh = false);

        //Task<IEnumerable<Contact>> GetItemsFilterAsync(string Filter = null, bool forceRefresh = false);
        //Task<IEnumerable<Contact>> GetNextItemsFilterAsync(int currentitemCount, string Filter = null, bool forceRefresh = false);
        //Task<IEnumerable<Contact>> GetItemsByTypeAsync(string ContactType, string Filter = null, bool forceRefresh = false);
        //Task<IEnumerable<Contact>> GetNextItemsByTypeAsync(int currentitemCount, string ContactType, string Filter = null, bool forceRefresh = false);
        ////Task<JObjectManualQuery> GetItemsByCommercialFilterAsync(string ContactType, List<CheckBoxItem> SelectedCommercials = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
        ////Task<JObjectManualQuery> GetNextItemsByCommercialFilterAsync(int currentitemCount, string ContactType, List<CheckBoxItem> SelectedCommercials = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
        ////Task<JObjectManualQuery> GetItemsByTypeFilterAsync(string ContactType, List<CheckBoxItem> SelectedTypes = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
        ////Task<JObjectManualQuery> GetNextItemsByTypeFilterAsync(int currentitemCount, string ContactType, List<CheckBoxItem> SelectedTypes = null, List<CheckBoxItem> SelectedResidences = null, bool forceRefresh = false);
        //Task<string> RewriteCustomFields(string contactId);
        ////Task<long> GetTotalCountByCollectSourceId(string collectSourceId);
        //Task<long> GetTotalCountByCollectSourceId(string collectSourceId, DateTime dateDeb, DateTime dateFin);
        //Task<long> GetTotalCountByCreationDateAsync(DateTime dateDeb, DateTime dateFin);
        //Task<IEnumerable<PicsStats>> GetContactStat(DateTime dateDeb, DateTime dateFin);
    }
}
