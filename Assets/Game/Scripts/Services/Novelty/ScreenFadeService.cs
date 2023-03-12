using DG.Tweening;
using Game.Models;
using Game.Models.Novelty;
using Gaze.Utilities;

namespace Game.Services.Novelty
{
    public class FadeService : BaseFadeService
    {
        readonly FadeContainer fadeContainer;

        protected override Tween Tween
        {
            get => fadeContainer.FadeTween.Value;
            set => fadeContainer.FadeTween.Value = value;
        }

        protected override float FadeInDuration => .5f;
        protected override float FadeOutDuration => .8f;

        public FadeService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
            fadeContainer = models.NoveltyContainer.FadeContainer;
        }

        public override void FadeIn()
        {
            base.FadeIn();
            fadeContainer.FadingOut.Value = false;
        }

        public override void FadeOut()
        {
            base.FadeOut();
            fadeContainer.FadingOut.Value = true;
        }

        protected override Tween CreateTween(
            float targetFade,
            float duration
        )
        {
            return DOTween.To(
                () => fadeContainer.Fade.Value,
                fade => fadeContainer.Fade.Value = fade,
                targetFade,
                duration);
        }
    }
}