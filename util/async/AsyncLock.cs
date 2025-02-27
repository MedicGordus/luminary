namespace luminary.util;


///<summary>
///
/// Portions of this class was originally written by Stephen Toub.
///
/// Most links are broken but the one that theComputerProgrammer@outlook.com used
///     to build the class was:
///     
///     https://devblogs.microsoft.com/pfxteam/building-async-coordination-primitives-part-6-asynclock/
///</summary>
public class AsyncLock
{
    private readonly SemaphoreSlim Semaphore;

    private readonly Task<Releaser> InternalReleaser;

    private AsyncLock(int _allowedThreads)
    {
        Semaphore = new SemaphoreSlim(_allowedThreads);
        InternalReleaser = Task.FromResult(new Releaser(this));
    }

    public Task<Releaser> LockAsync()
    {
        var wait = Semaphore.WaitAsync();

#nullable disable
        return wait.IsCompleted ?
            InternalReleaser :
            wait.ContinueWith((_, _state) => new Releaser((AsyncLock)_state),
                this, CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
#nullable enable
    }

    public struct Releaser : IDisposable
    {
        private readonly AsyncLock ToRelease;

        internal Releaser(AsyncLock _toRelease) { ToRelease = _toRelease; }

        public void Dispose()
        {
            if (ToRelease != null)
                ToRelease.Semaphore.Release();
        }
    }

    ///<param name="_allowedThreads">
    /// The amount of threads allowed through the lock at a time.
    ///</param>
    public static AsyncLock Create(int _allowedThreads = 1) => new AsyncLock(_allowedThreads);
}