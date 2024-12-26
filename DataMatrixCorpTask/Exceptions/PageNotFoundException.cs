namespace DataMatrixCorpTask.Exceptions;

public class PageNotFoundException : Exception
{
    public PageNotFoundException() : base("Provided page was not found")
    {
    }
}