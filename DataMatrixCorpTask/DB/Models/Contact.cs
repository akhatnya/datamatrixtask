namespace DataMatrixCorpTask.DB.Models;

// Refers to Contacts table
public class Contact
{
    public  Guid Id { get; set; }
    public String Firstname { get; set; }
    public String? Lastname { get; set; } // Usually Not Necessary
    public String PhoneNumber { get; set; }
    public String? Email { get; set; } // This is too
}