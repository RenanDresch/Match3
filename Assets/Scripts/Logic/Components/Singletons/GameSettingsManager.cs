using Game.Contracts;
using UnityEngine;

namespace Game.Logic
{
    [DefaultExecutionOrder(-100)]
    public class GameSettingsManager : MonoBehaviourSingleton<GameSettingsManager>
    {
        #region Fields

        [SerializeField]
        private GameConfigFile gameConfig = default;

        #endregion

        #region Properties

        public Sprite[] AvailableGems => gameConfig.AvailableGems;
        public int GridXSize => gameConfig.BoardColumns;
        public int GridYSize => gameConfig.BoardRows;

        public int InitialTargetScore => gameConfig.InitialTargetScore;
        public int TargetScoreMultiplier => gameConfig.TargetScoreMultiplier;

        public int TimerResetSeconds => gameConfig.TimerResetSeconds;

        #endregion

        #region Private Methods

        private void OnEnable()
        {
            Instance = this;
        }

        #endregion

        #region Public Methods

        public Sprite GetRandomGem()
        {
            return AvailableGems[Random.Range(0, AvailableGems.Length)];
        }

        #endregion
    }
}