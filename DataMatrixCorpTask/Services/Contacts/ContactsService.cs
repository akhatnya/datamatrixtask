using DataMatrixCorpTask.DB;
using DataMatrixCorpTask.DB.Models;
using DataMatrixCorpTask.Exceptions;
using DataMatrixCorpTask.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace DataMatrixCorpTask.Services.Contacts;

public class ContactsService : IContactsService
{
    private readonly DataMatrixDbContext _context;

    public ContactsService(DataMatrixDbContext context)
    {
        _context = context;
    }
        
    public async Task<ContactsDTO> AddContact(ContactsDTO contact)
    {
        var dbContact = new Contact()
        {
            Id = Guid.NewGuid(),
            Email = contact.Email,
            Firstname = contact.Firstname,
            Lastname = contact.Lastname,
            PhoneNumber = contact.PhoneNumber
        };

        try
        {
            await _context.Contacts.AddAsync(dbContact);
            await _context.SaveChangesAsync();
            contact.Id = dbContact.Id;
            // some logger actions
            return contact;
        }
        catch (Exception e)
        {
            // some logger actions
            throw;
        } // and optionally FINALLY to guarantee smth. (close streams etc.)
    }

    public async Task<ContactsDTO> UpdateContact(ContactsDTO contact)
    {
        var contactToUpdate = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);
        if (contactToUpdate == null) throw new ContactNotFoundException(); 
        
        try
        {
            contactToUpdate.Email = contact.Email;
            contactToUpdate.Firstname = contact.Firstname;
            contactToUpdate.Lastname = contact.Lastname;
            contactToUpdate.PhoneNumber = contact.PhoneNumber;

            await _context.SaveChangesAsync();
            return contact;
        }
        catch (Exception e)
        {
            // some logger actions
            throw;
        } // and optionally FINALLY to guarantee smth. (close streams etc.)
        
    }

    public async Task<List<ContactsDTO>> GetContactsList(int size, int page)
    {
        if (page < 1) throw new PageNotFoundException();
        
        try
        {
            var contactList = await _context.Contacts
                .AsNoTracking()
                .OrderBy(x => x.Firstname)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(x => new ContactsDTO()
                {
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber
                })
                .ToListAsync();

            return contactList;
        }
        catch (Exception e)
        {
            // some logger actions
            throw;
        } // and optionally FINALLY to guarantee smth. (close streams etc.)
    }

    public async Task<bool> RemoveContact(ContactsDTO contact)
    {
        var contactToDelete = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);
        if (contactToDelete == null) throw new ContactNotFoundException(); 
        
        try
        {
            _context.Contacts.Remove(contactToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            // some logger actions
            throw;
        } // and optionally FINALLY to guarantee smth. (close streams etc.)
    }
}