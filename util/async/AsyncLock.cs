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
    private readonly SemaphoreSlim m_semaphore;

    private readonly Task<Releaser> m_releaser;

    private AsyncLock(int allowedThreads)
    {
        m_semaphore = new SemaphoreSlim(allowedThreads);
        m_releaser = Task.FromResult(new Releaser(this));
    }

    public Task<Releaser> LockAsync()
    {
        var wait = m_semaphore.WaitAsync();
        return wait.IsCompleted ?
            m_releaser :
            wait.ContinueWith((_, state) => new Releaser((AsyncLock)state),
                this, CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    public struct Releaser : IDisposable
    {
        private readonly AsyncLock m_toRelease;

        internal Releaser(AsyncLock toRelease) { m_toRelease = toRelease; }

        public void Dispose()
        {
            if (m_toRelease != null)
                m_toRelease.m_semaphore.Release();
        }
    }

    ///<param name="allowedThreads">
    /// The amount of threads allowed through the lock at a time.
    ///</param>
    public static AsyncLock Create(int allowedThreads = 1) => new AsyncLock(allowedThreads);
}