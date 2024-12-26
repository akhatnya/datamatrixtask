using DataMatrixCorpTask.Exceptions;
using DataMatrixCorpTask.Models;
using DataMatrixCorpTask.Models.DTO;
using DataMatrixCorpTask.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace DataMatrixCorpTask.Controllers.v1;

[ApiController]
[Route("api/v1/contacts")]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _contactsService;

    public ContactsController(IContactsService contactsService)
    {
        _contactsService = contactsService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContactsDTO), 200)]
    [ProducesResponseType(typeof(ExceptionResponse), 404)]
    [ProducesResponseType(typeof(ExceptionResponse), 400)]
    public async Task<IActionResult> CreateContact(ContactsDTO contact)
    {
        try
        {
            var res = await _contactsService.AddContact(contact);
            return Ok(res);
        }
        catch (Exception e)
        {
            return BadRequest(new ExceptionResponse(e));
        }
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<ContactsDTO>), 200)]
    [ProducesResponseType(typeof(ExceptionResponse), 404)]
    [ProducesResponseType(typeof(ExceptionResponse), 400)]
    public async Task<IActionResult> GetContactsList(
        [FromQuery] int page = 1,
        [FromQuery] int size = Globals.ContactsDefaultPageSize
    )
    {
        try
        {
            var res = await _contactsService.GetContactsList(size, page);
            return Ok(res);
        }
        catch (PageNotFoundException e)
        {
            return NotFound(new ExceptionResponse(e));
        }
        catch (Exception e)
        {
            return BadRequest(new ExceptionResponse(e));
        }
    }

    [HttpPut] // we also can use HttpPatch, but here we dont provide additional information to model
    [ProducesResponseType(typeof(ContactsDTO), 200)]
    [ProducesResponseType(typeof(ExceptionResponse), 404)]
    [ProducesResponseType(typeof(ExceptionResponse), 400)]
    public async Task<IActionResult> EditContact(ContactsDTO contact)
    {
        try
        {
            var res = await _contactsService.UpdateContact(contact);
            return Ok(res);
        }
        catch (ContactNotFoundException e)
        {
            return NotFound(new ExceptionResponse(e));
        }
        catch (Exception e)
        {
            return BadRequest(new ExceptionResponse(e));
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(EmptyResult), 201)]
    [ProducesResponseType(typeof(ExceptionResponse), 404)]
    [ProducesResponseType(typeof(ExceptionResponse), 400)]
    public async Task<IActionResult> RemoveContact(ContactsDTO contact)
    {
        try
        {
            var res = await _contactsService.RemoveContact(contact);
            return Ok();
        }
        catch (ContactNotFoundException e)
        {
            return NotFound(new ExceptionResponse(e));
        }
        catch (Exception e)
        {
            return BadRequest(new ExceptionResponse(e));
        }
    }
}