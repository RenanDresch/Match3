using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Gaze.Utilities;
using UnityEngine.SceneManagement;

namespace Game.Services.SceneManagement
{
    public class SceneUnloadService : BaseService
    {

        public SceneUnloadService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
        }
        
        public async UniTask UnloadScenesAsync(
            string activeScene,
            CancellationToken cancellationToken
        )
        {
            var sceneUnloadOperations = new List<UniTask>();
        
            for (var i = 0;
                 i < SceneManager.sceneCount;
                 i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name != "Bootstrap" && scene.name != activeScene)
                {
                    sceneUnloadOperations.Add(SceneManager.UnloadSceneAsync(scene).ToUniTask(cancellationToken: cancellationToken));
                }
            }
            
            await UniTask.WhenAll(sceneUnloadOperations);
        }
    }
}