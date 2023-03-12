using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Models;
using Game.Services.Novelty;
using Gaze.Utilities;
using UnityEngine.SceneManagement;

namespace Game.Services.SceneManagement
{
    public class SceneSwitchService : BaseService
    {
        readonly ReusableCancellationToken cancellationTokenSource;
        readonly SceneLoadService sceneLoadService;
        readonly SceneUnloadService sceneUnloadService;
        readonly FadeService fadeService;

        public SceneSwitchService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
            cancellationTokenSource = new ReusableCancellationToken(destroyable);
            sceneLoadService = new SceneLoadService(
                destroyable,
                models);
            sceneUnloadService = new SceneUnloadService(
                destroyable,
                models);
            fadeService = new FadeService(
                destroyable,
                models);

            models.SceneContainer.SceneName.SafeBindOnChangeActionWithInvocation(
                destroyable,
                OnSceneChange);
        }

        void OnSceneChange(
            string targetScene
        )
        {
            if (string.IsNullOrEmpty(targetScene) || targetScene == SceneManager.GetActiveScene().name)
            {
                ApplicationLogger.WithLevel(LogLevel.Debug)?.Log($"Wont switch scene: not a scene? {string.IsNullOrEmpty(targetScene)} | already loaded {targetScene == SceneManager.GetActiveScene().name}");
                return;
            }

            UniTask.Void(
                () => SwitchSceneAsync(
                    targetScene,
                    cancellationTokenSource.NewCancellationToken));
        }

        async UniTaskVoid SwitchSceneAsync(
            string targetScene,
            CancellationToken cancellationToken
        )
        {
            ApplicationLogger.WithLevel(LogLevel.Debug)?.Log($"Switching to scene {targetScene}");

            ApplicationLogger.WithLevel(LogLevel.Silly)?.Log("Awaiting for fade out before unloading scenes...");
            await FadeOutAsync();

            await sceneUnloadService.UnloadScenesAsync(
                targetScene,
                cancellationToken);

            ApplicationLogger.WithLevel(LogLevel.Silly)?.Log("Scenes unloaded");

            await sceneLoadService.LoadSceneAsync(
                targetScene,
                cancellationToken);

            ApplicationLogger.WithLevel(LogLevel.Silly)?.Log($"Scene {targetScene} loaded");

            SetActiveScene(targetScene);

            fadeService.FadeIn();

            ApplicationLogger.WithLevel(LogLevel.Debug)?.Log("Scenes switch complete!");
        }

        async UniTask FadeOutAsync()
        {
            fadeService.FadeOut();
            while (Models.NoveltyContainer.FadeContainer.Fade.Value < 1)
            {
                await UniTask.NextFrame();
            }
        }

        static void SetActiveScene(
            string targetScene
        )
        {
            var scene = SceneManager.GetSceneByName(targetScene);
            if (scene.IsValid())
            {
                SceneManager.SetActiveScene(scene);
            }
            else
            {
                throw new InvalidOperationException($"Failed to set active scene: {targetScene}");
            }
        }
    }
}