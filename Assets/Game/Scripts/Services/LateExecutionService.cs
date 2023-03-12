using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Gaze.Utilities;

namespace Game.Services
{
    public class LateExecutionService : BaseService
    {
        public LateExecutionService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models) { }

        public void LateExecute(
            Action action
        )
        {
            UniTask.Void(
                () => LateExecuteAsync(
                    action,
                    ServiceCancellationToken));
        }

        static async UniTaskVoid LateExecuteAsync(
            Action action,
            CancellationToken cancellationToken
        )
        {
            await UniTask.NextFrame(cancellationToken);
            action();
        }
    }
}