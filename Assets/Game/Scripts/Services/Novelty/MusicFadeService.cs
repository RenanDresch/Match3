using DG.Tweening;
using Game.Models;
using Game.Models.Novelty;
using Gaze.Utilities;

namespace Game.Services.Novelty
{
    public class MusicService : BaseFadeService
    {
        readonly AudioContainer audioContainer;
        
        protected override Tween Tween
        {
            get => audioContainer.MusicTween.Value;
            set => audioContainer.MusicTween.Value = value;
        }

        protected override float FadeInDuration => .5f;
        protected override float FadeOutDuration => .5f;
        
        public MusicService(
                IDestroyable destroyable,
                ModelsRepositoryWrapper models
        ) : base(
                destroyable,
                models)
        {
            audioContainer = models.NoveltyContainer.AudioContainer;
            
            models.NoveltyContainer.FadeContainer.FadingOut.SafeBindOnChangeActionWithInvocation(
                destroyable,
                OnFadingOutStateChange);
        }
        
        void OnFadingOutStateChange(
            bool fadingOut
        )
        {
            if (fadingOut)
            {
                FadeOut();
            }
            else
            {
                FadeIn();
            }
        }
        
        protected override Tween CreateTween(
            float targetFade,
            float duration
        )
        {
            return audioContainer.MusicPlayer.Value.DOFade(targetFade, duration);
        }
    }
}