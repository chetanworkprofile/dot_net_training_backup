using Microsoft.AspNetCore.Mvc;
using ContactsApi.Modals;
using ContactsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Services
{
    public class Service:IService
    {
        private readonly ContactsAPIDbContext DbContext;
        public Service(ContactsAPIDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<List<Contact>> GetContacts()
        {
            return await DbContext.Contacts.ToListAsync();
        }
        public async Task<Contact> GetContact(Guid id)
        {
            var contact = await DbContext.Contacts.FindAsync(id);
            return contact;
        }
        public async Task<Contact> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };
            await DbContext.Contacts.AddAsync(contact);
            await DbContext.SaveChangesAsync();
            return contact;
        }
        public async Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await DbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;

                await DbContext.SaveChangesAsync();
            }
            return contact;
        }
        public async Task<int> DeleteContact(Guid id)
        {
            var contact = await DbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                DbContext.Remove(contact);
                await DbContext.SaveChangesAsync();
                return 0;
            }
            return -1;
        }
        public List<Contact> SortData(string onWhatBasis)
        {
            var sorted = DbContext.Contacts.AsEnumerable();

            //var sorted = new ContactsAPIDbContext(null);
            /*sorted = from contact in DbContext.Contacts
                     orderby contact.FullName
                     select contact;*/
            Func<Contact, Object> orderBy = x => x.Id;
            if (onWhatBasis == "id")
            {
                orderBy = x => x.Id;
            }
            else if (onWhatBasis == "FullName")
            {
                orderBy = x => x.FullName;
            }
            else if (onWhatBasis == "Email")
            {
                orderBy = x => x.Email;
            }
            else if (onWhatBasis == "Address")
            {
                orderBy = x => x.Address;
            }
            if (orderBy != null)
            {
                sorted = DbContext.Contacts.OrderBy(orderBy).Select(c => (c));
            }
            return sorted.ToList();
        }
        public async Task<List<Contact>> Search(string searchString)
        {
            var result = DbContext.Contacts.FromSqlRaw($"select * from Contacts where FullName Like '%{searchString}%' or Address Like '%{searchString}%' or Email Like '%{searchString}%' or Phone Like '%{searchString}%'");
            return await result.ToListAsync();
        }
        public async Task<List<Contact>> GetPages(int RecordsPerPage, int PageNumber)
        {
            var query = DbContext.Contacts.AsQueryable();
            var result = query.Skip((PageNumber - 1) * RecordsPerPage).Take(RecordsPerPage).ToListAsync();
            return await result;
        }
    }
}
