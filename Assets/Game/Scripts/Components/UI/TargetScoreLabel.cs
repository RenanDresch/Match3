using DG.Tweening;

namespace Game.Components.UI
{
    public class TargetScoreLabel : BaseLabelComponent
    {
        Sequence bounceSequence;

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            models.GameLoopContainer.TargetScore.SafeBindOnChangeActionWithInvocation(
                this,
                OnTargetScoreChange);
        }
        void OnTargetScoreChange(
            int targetScore
        )
        {
            label.text = targetScore.ToString();
            AnimateTargetScoreChange();
        }
        
        void AnimateTargetScoreChange()
        {
            bounceSequence?.Kill();
            bounceSequence = DOTween.Sequence();

            bounceSequence.Append(transform.DOScale(1.4f, .2f)
                .SetEase(Ease.Linear));

            bounceSequence.Append(transform.DOScale(1f, .5f)
                .SetEase(Ease.OutBounce));
        }
    }
}