namespace DataMatrixCorpTask.Exceptions;

public class ContactNotFoundException : Exception
{
    public ContactNotFoundException() : base("Contact was not found")
    {
    }
}