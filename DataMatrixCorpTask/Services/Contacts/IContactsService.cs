using DataMatrixCorpTask.DB.Models;
using DataMatrixCorpTask.Models.DTO;

namespace DataMatrixCorpTask.Services.Contacts;

public interface IContactsService
{
    Task<ContactsDTO> AddContact(ContactsDTO contact);
    Task<ContactsDTO> UpdateContact(ContactsDTO contact);
    Task<List<ContactsDTO>> GetContactsList(int size, int page);
    Task<bool> RemoveContact(ContactsDTO contact);
}