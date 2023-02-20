using Microsoft.AspNetCore.Mvc;
using ContactsApi.Modals;

namespace ContactsApi.Services
{
    public interface IService
    {
        public Task<List<Contact>> GetContacts();
        public Task<Contact> GetContact(Guid id);
        public Task<Contact> AddContact(AddContactRequest addContactRequest);
        public Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest);
        public Task<int> DeleteContact(Guid id);
        public List<Contact> SortData(string onWhatBasis);
        public Task<List<Contact>> Search(string searchString);
        public Task<List<Contact>> GetPages(int RecordsPerPage, int PageNumber);
    }
}
