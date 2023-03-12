using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.FX
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
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
            button.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            GameMusicManager.Instance.SetMusicVolume(.2f);
            fader.Fade(1).OnComplete(Application.Quit);
        }

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        #endregion
    }
}