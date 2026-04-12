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
    /// <summary>
    /// This holds the list of exceptions.
    /// </summary>
    protected List<string> ListOfExceptionStrings;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ExceptionList()
    {
        ListOfExceptionStrings = new();
    }

    /// <summary>
    /// Adds an error message to the list.
    /// </summary>
    /// <param name="_exceptionString">The message for the added error.</param>
    public void AddExceptionString(string _exceptionString)
    {
        ListOfExceptionStrings.Add(_exceptionString);
    }

    /// <summary>
    /// Creates a new instance of the disposable object that throws the exceptions on the disposal, if thre are any logged error messages.
    /// </summary>
    /// <returns>Reference to the IDisposable.</returns>
    public Releaser ThrowIfExceptions() => new(this);

    /// <summary>
    /// Helper that inherits IDisposable so it can throw the exceptions at the end of the using block.
    /// </summary>
    /// <param name="_exceptionList">Reference to the object that creates the Releaser (so the list can be accessed).</param>
    public readonly struct Releaser(ExceptionList _exceptionList) : IDisposable
    {
        // Reference to the object that creates the Releaser (so the list can be accessed).   
        readonly ExceptionList EList = _exceptionList;

        /// <summary>
        /// Function that is called upon disposal (as per IDisposable).
        /// </summary>
        public readonly void Dispose()
        {
            // check if any errors were addded
            if (EList?.ListOfExceptionStrings?.Count is > 0)
            {
                // entrance indicates all of the following:
                //  EList is not null
                //  EList.ListOfExceptionStrings is not null
                //  EList.ListOfExceptionStrings.Count > 0 (Count can never be less either)

                throw new Exception(string.Join(Environment.NewLine, EList.ListOfExceptionStrings));
            }
        }

        /// <summary>
        /// Adds an error message to the list.
        /// </summary>
        /// <param name="_exceptionString">The message for the added error.</param>
        public void AddExceptionString(string _exceptionString)
        {
            EList.ListOfExceptionStrings.Add(_exceptionString);
        }
    }
}