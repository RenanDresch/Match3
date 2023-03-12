using DG.Tweening;
using Game.FX;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic
{
    public class GameLoopController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private GameTimer timer = default;
        [SerializeField]
        private GridController gridController = default;
        [SerializeField]
        private ScoreController scoreController = default;
        [SerializeField]
        private SelectionController selector = default;
        [SerializeField]
        private GameCamera gameCamera = default;

        [SerializeField]
        private GeneralAnimator animator = default;
        [SerializeField]
        private SoundFXController soundFXController = default;

        [SerializeField]
        private CanvasGroup gameOverScreen = default;

        [SerializeField]
        private Button gameResetButton = default;

        [SerializeField]
        private TMP_Text finalScoreLabel = default;

        private IEnumerator gameBoostrapCoroutine;

        #endregion

        #region PrivateMethods

        private void Start()
        {
            gameCamera.SetupCamera();

            BootstrapGame();
            timer.OnTimeUp += GameOver;

            gameResetButton.onClick.AddListener(BootstrapGame);
        }

        private void OnDestroy()
        {
            if(timer)
            {
                timer.OnTimeUp -= GameOver;
            }
        }

        private void BootstrapGame()
        {
            GameMusicManager.Instance.SetMusicVolume(1, 1);

            scoreController.ResetScore();

            gameOverScreen.blocksRaycasts = false;
            gameOverScreen.interactable = false;

            animator.FadeCanvasOut(gameOverScreen);

            if (gameBoostrapCoroutine != null)
            {
                StopCoroutine(gameBoostrapCoroutine);
            }
            gameBoostrapCoroutine = BootstrapCoroutine();
            StartCoroutine(gameBoostrapCoroutine);
        }

        private IEnumerator BootstrapCoroutine()
        {
            yield return new WaitForSecondsRealtime(1);

            timer.ResetTimer();
            gridController.BuildGrid();
            scoreController.ResetTargetScore();

            yield return new WaitForSecondsRealtime(0.8f);

            timer.StartTimer();
            soundFXController.PlayClear();
            selector.PlayingGame = true;

            var gemRenderers = gridController.AllGems
                .Select(x => x.SpriteRenderer).ToArray();

            animator.AnimateGemSpawn(gemRenderers);
        }

        private void GameOver()
        {
            GameMusicManager.Instance.SetMusicVolume(0.25f, 1);

            finalScoreLabel.text = $"{scoreController.Score}";

            gameOverScreen.blocksRaycasts = true;
            gameOverScreen.interactable = true;
            animator.FadeCanvasIn(gameOverScreen);

            var gemRenderers = gridController.AllGems
                .Select(x => x.SpriteRenderer).ToArray();

            animator.AnimateGemFade(gemRenderers);

            selector.PlayingGame = false;
        }

        #endregion
    }
}