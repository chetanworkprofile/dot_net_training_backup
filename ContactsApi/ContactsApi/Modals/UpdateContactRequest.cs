namespace ContactsApi.Modals
{
    public class UpdateContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
// we could easily use the AddContactRequest class to serve the purpose but are
// creating a new one for updation because the name is different and use case is different to 
// be more clear and clean code