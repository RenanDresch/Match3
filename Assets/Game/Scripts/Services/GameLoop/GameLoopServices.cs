using Game.Models;
using Gaze.Utilities;

namespace Game.Services.GameLoop
{
    public class GameLoopServices : BaseService
    {
        readonly GameStartService gameStartService;
        readonly TimerCountDownService timerCountDownService;
        readonly GameOverService gameOverService;
        
        public GameLoopServices(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
            gameStartService = new GameStartService(
                destroyable,
                models);

            timerCountDownService = new TimerCountDownService(
                destroyable,
                models);

            gameOverService = new GameOverService(
                destroyable,
                models);
        }
    }
}