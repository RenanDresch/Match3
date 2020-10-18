using DG.Tweening;
using UnityEngine;

namespace Game.FX
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenFader : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private CanvasGroup canvasGroup = default;

        #endregion

        #region Private Methods

        private void Awake()
        {
            Fade(0);
        }

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        #endregion

        #region Public Methods

        public Tween Fade(float value, float time = 2)
        {
            canvasGroup.blocksRaycasts = value >= 0.5f;
            return canvasGroup.DOFade(value, time);
        }

        #endregion
    }
}