using DG.Tweening;
using Game.Models;
using Gaze.Utilities;

namespace Game.Services.Novelty
{
    public abstract class BaseFadeService : BaseService
    {
        protected abstract Tween Tween { get; set; }
        
        protected abstract float FadeInDuration { get; }
        protected abstract float FadeOutDuration { get; }
        
        public BaseFadeService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {}
        
        public virtual void FadeIn()
        {
            KillCurrentTween();
            SetFadeTween(0, FadeInDuration);
        }

        public virtual void FadeOut()
        {
            KillCurrentTween();
            SetFadeTween(1, FadeOutDuration);
        }

        void KillCurrentTween()
        {
            Tween?.Kill();
        }

        void SetFadeTween(
            float targetFade,
            float duration
        )
        {
            Tween = CreateTween(targetFade, duration);
        }

        protected abstract Tween CreateTween(
            float targetFade,
            float duration
        );
    }
}