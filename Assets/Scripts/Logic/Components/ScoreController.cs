using Game.FX;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Logic
{
    public class ScoreController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TMP_Text scoreLabel = default;
        [SerializeField]
        private TMP_Text targetScoreLabel = default;

        [SerializeField]
        private SelectionController selector = default;

        [SerializeField]
        private GameTimer timer = default;

        [SerializeField]
        private GeneralAnimator animator = default;

        private int currentScore = 0;
        private int targetScore = 0;

        #endregion

        #region Properties

        public int Score => currentScore;

        #endregion

        #region Private Methods

        private void Start()
        {
            selector.OnCombo += UpdateScore;
        }

        private void OnDestroy()
        {
            if(selector)
            {
                selector.OnCombo -= UpdateScore;
            }
        }

        private void UpdateScore(List<List<Gem>> combos)
        {
            animator.PunchText(scoreLabel.transform);

            var scoreMultiplier = combos.Count;
            var comboScore = 0;
            foreach (var combo in combos)
            {
                comboScore += combo.Count;
            }

            currentScore += comboScore * scoreMultiplier;

            if(currentScore >= targetScore)
            {
                timer.ResetTimer();
                targetScore *= GameSettingsManager.Instance.TargetScoreMultiplier;
                UpdateScoreTarget();
            }

            scoreLabel.text = $"{currentScore}";
        }

        private void UpdateScoreTarget()
        {
            animator.BounceText(targetScoreLabel.transform);
            targetScoreLabel.text = $"{targetScore}";
        }

        #endregion

        #region Public Methods

        public void ResetTargetScore()
        {
            targetScore = GameSettingsManager.Instance.InitialTargetScore;
            UpdateScoreTarget();
        }

        public void ResetScore()
        {
            currentScore = 0;
            targetScore = 0;

            scoreLabel.text = $"{currentScore}";
            targetScoreLabel.text = $"{targetScore}";
        }

        #endregion
    }
}