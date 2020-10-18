using Game.Contracts;
using UnityEngine;

namespace Game.Logic
{
    [DefaultExecutionOrder(-100)]
    public class GameSettingsManager : MonoBehaviourSingleton<GameSettingsManager>
    {
        #region Fields

        [SerializeField]
        private Sprite[] availableGems = default;

        [SerializeField]
        private int gridXSize = default;
        [SerializeField]
        public int gridYSize = default;

        #endregion

        #region Properties

        public Sprite[] AvailableGems => availableGems;

        public int GridXSize => gridXSize;
        public int GridYSize => gridYSize;

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