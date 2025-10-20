using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace luminary.util;

/// <summary>
/// Helper class that allows callers to use "using" and that will throw an exception at the end of the using if one or more exceptions are logged.
/// 
/// Usage:
///
///     using (var el = new ExceptionList().ThrowIfExceptions())
///     {
///         if(tinyError)
///         {
///             el.AddExceptionString("tiny error.");
///         }
/// 
///         if(bigError)
///         {
///             el.AddExceptionString("big error.");
///         }
///     } <-- exception will be thrown here if there was 1+ errors
/// </summary>
public class ExceptionList
{
    protected List<string> ListOfExceptionStrings;

    public ExceptionList()
    {
        ListOfExceptionStrings = new();
    }

    public void AddExceptionString(string _exceptionString)
    {
        ListOfExceptionStrings.Add(_exceptionString);
    }

    public Releaser ThrowIfExceptions() => new(this);

    public readonly struct Releaser(ExceptionList _exceptionList) : IDisposable
    {
        readonly ExceptionList EList = _exceptionList;

        public readonly void Dispose()
        {
            if (EList != null)
            {
                if (EList.ListOfExceptionStrings.Count != 0)
                {
                    throw new Exception(string.Join(Environment.NewLine, EList.ListOfExceptionStrings));
                }
            }
        }

        public void AddExceptionString(string _exceptionString)
        {
            EList.ListOfExceptionStrings.Add(_exceptionString);
        }
    }
}