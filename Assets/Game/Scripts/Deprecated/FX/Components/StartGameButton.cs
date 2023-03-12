using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.FX
{
    [RequireComponent(typeof(Button))]
    public class StartGameButton : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Button button = default;

        [SerializeField]
        private ScreenFader fader = default;

        #endregion

        #region Private Methods

        private void Awake()
        {
            button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            GameMusicManager.Instance.SetMusicVolume(.2f);
            fader.Fade(1).OnComplete(LoadLevel);
        }

        private void LoadLevel()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene())
                .completed += (operation) =>
                {
                    SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
                };
        }

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        #endregion
    }
}