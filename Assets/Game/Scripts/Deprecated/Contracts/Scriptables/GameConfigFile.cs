using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Contracts
{
    [CreateAssetMenu(fileName = "New Game Config File", menuName = "Game/New Config File")]

    public class GameConfigFile : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private Sprite[] availableGems = default;

        [SerializeField]
        private int initialTargetScore = 20;

        [SerializeField]
        private int targetScoreMultiplier = 2;

        [SerializeField]
        private int boardRows = 8;

        [SerializeField]
        private int boardColumns = 8;

        [SerializeField]
        private int timerResetSeconds = 120;

        #endregion

        #region Properties

        public Sprite[] AvailableGems => availableGems;

        public int InitialTargetScore => initialTargetScore;

        public int TargetScoreMultiplier => targetScoreMultiplier;

        public int BoardRows => boardRows;

        public int BoardColumns => boardColumns;

        public int TimerResetSeconds => timerResetSeconds;

        #endregion
    }
}