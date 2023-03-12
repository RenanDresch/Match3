using Game.Services.Bootstrap;
using Game.Services.Runtime;

namespace Game.Components
{
    public class GameBootstrapper : BaseComponent
    {
        RuntimeServices runtimeServices;

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();

            new BootstrapService(
                this,
                models).InstantiateBootstrapObjects();

            runtimeServices = new RuntimeServices(
                this,
                models);

            models.SceneContainer.SceneName.Value = models.GameStaticConfigsContainer.MenuSceneName;
        }
    }
}