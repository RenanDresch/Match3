using Game.Models;
using Game.Models.App;
using Gaze.Utilities;

namespace Game.Services.GameLoop
{
    public class GameStartService : BaseService
    {
        public GameStartService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
            ResetGameLoopState();
        }
        
        void ResetGameLoopState()
        {
            Models.AppContainer.GameState.Value = GameState.Preparing;
            Models.GameLoopContainer.Score.Value = 0;
            Models.GameLoopContainer.SecondsLeft.Value = Models.GameConfigsContainer.InitialAvailableSeconds.Value;
            Models.GameLoopContainer.TargetScore.Value = Models.GameConfigsContainer.InitialTargetScore.Value;
        }

        public void StartGame()
        {
            Models.AppContainer.GameState.Value = GameState.Playing;
        }
    }
}