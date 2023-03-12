using Game.Models;
using Game.Models.App;
using Gaze.Utilities;

namespace Game.Services.GameLoop
{
    public class GameOverService : BaseService
    {
        public GameOverService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
            models.AppContainer.GameState.SafeBindOnChangeActionWithInvocation(
                destroyable,
                OnGameStateChange);
        }
        
        void OnGameStateChange(
            GameState gameState
        )
        {
            if (gameState is GameState.Playing)
            {
                Models.AppContainer.GameState.UnbindOnChangeAction(OnGameStateChange);
                Models.GameLoopContainer.SecondsLeft.SafeBindOnChangeActionWithInvocation(
                    Destroyable,
                    OnCountDown);
            }
        }
        
        void OnCountDown(
            int secondsLeft
        )
        {
            if (secondsLeft == 0)
            {
                Models.AppContainer.GameState.Value = GameState.GameOver;
            }
        }
    }
}