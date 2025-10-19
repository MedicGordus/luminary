namespace luminary.util
{
    public enum Severity : int
    {
        z_error = 0,
        UnexpectedIssue = 1,
        AnnoyingIssue = 2,
        Informational = 3
    }
        
    public class Log
    {

        public async Task LogEntryAsync(Severity _severity, string _message)
        {
            //
            throw new NotImplementedException();
        }
    }
}