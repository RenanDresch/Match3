using System;
using DG.Tweening;

namespace Game.Components.UI
{
    public class TimerLabel : BaseLabelComponent
    {
        Sequence punchSequence;

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            models.GameLoopContainer.SecondsLeft.SafeBindOnChangeActionWithInvocation(
                this,
                OnCountDown);
        }
        void OnCountDown(
            int secondsLeft
        )
        {
            var timeSpan = new TimeSpan(
                0,
                0,
                0,
                secondsLeft);

            label.text = $"{timeSpan.Minutes:00} : {timeSpan.Seconds:00}";
            AnimateTargetScoreChange();
        }
        
        void AnimateTargetScoreChange()
        {
            punchSequence?.Kill();
            punchSequence?.Kill();
            punchSequence = DOTween.Sequence();

            punchSequence.Append(transform.DOScale(1.05f, .05f)
                .SetEase(Ease.OutCirc));

            punchSequence.Append(transform.DOScale(1f, .05f)
                .SetEase(Ease.OutCirc));
        }
    }
}