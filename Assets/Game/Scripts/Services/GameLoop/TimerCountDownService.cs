using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Models.App;
using Gaze.Utilities;

namespace Game.Services.GameLoop
{
    public class TimerCountDownService : BaseService
    {
        public TimerCountDownService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
            UniTask.Void(CountDownAsync, ServiceCancellationToken);
        }

        async UniTaskVoid CountDownAsync(CancellationToken cancellationToken)
        {
            while(Models.GameLoopContainer.TimerState.Value != TimerState.CountingDown)
            {
                await UniTask.NextFrame(cancellationToken);
            }

            Models.GameLoopContainer.SecondsLeft.Value--;

            await UniTask.Delay(
                1000,
                cancellationToken: cancellationToken);
        }
    }
}