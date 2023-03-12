using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Gaze.Utilities;
using UnityEngine.SceneManagement;

namespace Game.Services.SceneManagement
{
    public class SceneLoadService : BaseService
    {
        public SceneLoadService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
        }

        public async UniTask LoadSceneAsync(
            string targetScene,
            CancellationToken cancellationToken
        )
        {
            await SceneManager.LoadSceneAsync(
                targetScene,
                LoadSceneMode.Additive).WithCancellation(cancellationToken);
        }
    }
}