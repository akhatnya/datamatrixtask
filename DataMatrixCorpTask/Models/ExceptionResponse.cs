namespace DataMatrixCorpTask.Models;

public class ExceptionResponse
{
    public ExceptionResponse(Exception e)
    {
        Message = e.Message;
    }
    public string Message { get; set; }
}