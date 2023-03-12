using Game.Services.GameLoop;

namespace Game.Components.GameLoop
{
    public class GameLoopController : BaseComponent
    {
        GameLoopServices gameLoopServices;
        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            gameLoopServices = new GameLoopServices(
                this,
                models);
        }
    }
}