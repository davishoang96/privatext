namespace privatext.Common.Response;

public class BaseEndpointResponse
{
    private readonly IList<string> errors = new List<string>();

    public IList<string> Errors => errors;

    public bool Success => !Errors.Any();

    public void AddError(string errorMessage)
    {
        errors.Add(errorMessage);
    }
}
