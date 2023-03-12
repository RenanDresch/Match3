using System;
using Game.Models.App;
using Game.Models.App.Scenes;
using Game.Models.Configs;
using Game.Models.GameLoop;
using Game.Models.Novelty;
using UnityEngine.AddressableAssets;

namespace Game.Models
{
    [Serializable]
    public class ModelsRepositoryWrapper
    {
        public AssetReferenceT<ModelsRepository> repositoryAddress;

        Action completionCallback;

        #region Containers

        public GameStaticConfigsContainer GameStaticConfigsContainer { get; private set; }
        public GameConfigsContainer GameConfigsContainer { get; private set; }
        public GamePrefabsContainer GamePrefabsContainer { get; private set; }
        public GameLoopContainer GameLoopContainer { get; private set; }
        
        public EntitiesContainer Entities { get; private set; }
        public AppContainer AppContainer { get; private set; }
        public SceneContainer SceneContainer { get; private set; }
        public NoveltyContainer NoveltyContainer { get; private set; }

        #endregion

        public ModelsRepositoryWrapper(
            string repositoryGuid
        )
        {
            repositoryAddress = new AssetReferenceT<ModelsRepository>(repositoryGuid);
        }

        public ModelsRepositoryWrapper(
            ModelsRepository modelsRepository
        )
        {
            ModelsRepository = modelsRepository;
            Prepare(
                modelsRepository,
                null);
        }

        public bool Loaded { get; private set; }

        public ModelsRepository ModelsRepository { get; private set; }

        public void Load(
            Action onLoadCallback
        )
        {
            if (string.IsNullOrEmpty(repositoryAddress.AssetGUID))
            {
                return;
            }

            if (Loaded || ModelsRepository != null)
            {
                onLoadCallback();
            }

            repositoryAddress.LoadAssetAsync().Completed += handle =>
            {
                Prepare(
                    handle.Result,
                    onLoadCallback);
            };
        }

        void Prepare(
            ModelsRepository models,
            Action callback
        )
        {
            ModelsRepository = models;
            CachePropertyPaths();
            Loaded = true;
            callback?.Invoke();
            completionCallback?.Invoke();
            completionCallback = null;
        }

        //Todo: It might be possible to do this through reflection + attributes
        void CachePropertyPaths()
        {
            GameStaticConfigsContainer = ModelsRepository.GameStaticConfigsContainer;
            GameConfigsContainer = ModelsRepository.GameConfigsContainer;
            GamePrefabsContainer = ModelsRepository.GamePrefabsContainer;
            GameLoopContainer = ModelsRepository.GameLoopContainer;

            Entities = ModelsRepository.EntitiesContainer;
            AppContainer = ModelsRepository.AppContainer;
            SceneContainer = AppContainer.SceneContainer;
            NoveltyContainer = ModelsRepository.NoveltyContainer;
        }

        public void AddLoadCallback(
            Action callback
        )
        {
            if (ModelsRepository)
            {
                callback();
            }
            else
            {
                completionCallback += callback;
            }
        }

        public static implicit operator ModelsRepository(
            ModelsRepositoryWrapper wrapper
        )
        {
            return wrapper.ModelsRepository;
        }
    }
}