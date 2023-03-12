using UnityEngine;

namespace Game.Components.Novelty
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : BaseComponent
    {
        [SerializeField, HideInInspector]
        CanvasGroup canvasGroup;
        
        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            models.NoveltyContainer.FadeContainer.Fade.SafeBindOnChangeActionWithInvocation(
                this,
                OnFadeChange);

            models.NoveltyContainer.FadeContainer.FadingOut.SafeBindOnChangeActionWithInvocation(
                this,
                OnFadingOutStateChange);
        }
        void OnFadingOutStateChange(
            bool fadingOut
        )
        {
            canvasGroup.interactable = fadingOut;
            canvasGroup.blocksRaycasts = fadingOut;
        }

        void OnFadeChange(
            float fadeValue
        )
        {
            canvasGroup.alpha = fadeValue;
        }
        
        #if UNITY_EDITOR

        void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        #endif
    }
}