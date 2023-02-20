using ContactsApi.Modals;
using ContactsApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactsApi.Services;
using System;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        Service serviceInstance;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            serviceInstance = new Service(dbContext);
        }

     

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await serviceInstance.GetContacts());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await serviceInstance.GetContact(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            return Ok(await serviceInstance.AddContact(addContactRequest));
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = serviceInstance.UpdateContact(id, updateContactRequest);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(await contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            Task<int> isDeleted = serviceInstance.DeleteContact(id);
            if(isDeleted.Result == 0)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/sort")]
        public IActionResult SortData(string onWhatBasis)
        {
            var sorted = serviceInstance.SortData(onWhatBasis);
            return Ok(sorted.ToList());
        }

        [HttpGet]
        [Route("/search")]
        public async Task<IActionResult> Search(string searchString)
        {
            return Ok(await serviceInstance.Search(searchString));
        }

        [HttpGet]
        [Route("/Pagination")]
        public async Task<IActionResult> GetPages(int RecordsPerPage, int PageNumber)
        {
           return Ok(await serviceInstance.GetPages(RecordsPerPage, PageNumber));
        }
    }
}
