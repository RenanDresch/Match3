using Game.Models;
using Game.Models.App;
using Gaze.Utilities;

namespace Game.Services.GameLoop
{
    public class TimerStateService : BaseService
    {
        public TimerStateService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
            models.AppContainer.GameState.SafeBindOnChangeActionWithInvocation(
                destroyable,
                OnGameStateChange);
            models.NoveltyContainer.GameBoardAnimating.SafeBindOnChangeActionWithInvocation(
                destroyable,
                OnAnimatingStateChange);
        }

        void OnGameStateChange(
            GameState gameState
        )
        {
            Models.GameLoopContainer.TimerState.Value = AccordingToGameState(gameState);
        }

        void OnAnimatingStateChange(
            bool animating
        )
        {
            Models.GameLoopContainer.TimerState.Value = animating
                ? TimerState.Stopped
                : AccordingToGameState(Models.AppContainer.GameState.Value);
        }
        
        TimerState AccordingToGameState(
            GameState gameState
        )
        {
            return gameState is GameState.Playing
                ? TimerState.CountingDown
                : TimerState.Stopped;
        }
    }
}