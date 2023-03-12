using System.Threading;
using Gaze.Utilities;

namespace Game
{
    public class ReusableCancellationToken
    {
        CancellationTokenSource cancellationTokenSource;

        public ReusableCancellationToken(
            IDestroyable destroyable
        )
        {
            destroyable.OnDestroyEvent += DisposeCurrentCancellationToken;
        }

        public CancellationToken CurrentCancellationToken => GetCurrentCancellationToken();
        public CancellationToken NewCancellationToken => GetNewCancellationToken();

        CancellationToken GetCurrentCancellationToken()
        {
            if (cancellationTokenSource == null)
            {
                return RefreshCancellationTokenSource()
                       .Token;
            }

            return cancellationTokenSource.Token;
        }

        CancellationToken GetNewCancellationToken()
        {
            RefreshCancellationTokenSource();
            return cancellationTokenSource.Token;
        }

        CancellationTokenSource RefreshCancellationTokenSource()
        {
            DisposeCurrentCancellationToken();
            cancellationTokenSource = new CancellationTokenSource();
            return cancellationTokenSource;
        }

        public void DisposeCurrentCancellationToken()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }
    }
}