using Game.Models;
using Game.Models.App;
using Gaze.Utilities;
using UnityEngine;

namespace Game.Services.Bootstrap
{
    public class BootstrapService : BaseService
    {
        public BootstrapService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
            models.AppContainer.GameState.Value = GameState.Menu;
        }

        public void InstantiateBootstrapObjects()
        {
            Object.Instantiate(Models.GamePrefabsContainer.GameCameraPrefab);
            Object.Instantiate(Models.GamePrefabsContainer.MusicPlayerPrefab);
            Object.Instantiate(Models.GamePrefabsContainer.EventSystem);
        }
    }
}