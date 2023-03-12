using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Components.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreLabel : BaseLabelComponent
    {
        Sequence punchSequence;

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            models.GameLoopContainer.Score.SafeBindOnChangeActionWithInvocation(
                this,
                OnScoreChange);
        }
        void OnScoreChange(
            int score
        )
        {
            label.text = score.ToString();
            AnimateScoreChange();
        }
        
        void AnimateScoreChange()
        {
            punchSequence?.Kill();
            punchSequence = DOTween.Sequence();

            punchSequence.Append(transform.DOScale(1.05f, .05f)
                .SetEase(Ease.OutCirc));

            punchSequence.Append(transform.DOScale(1f, .05f)
                .SetEase(Ease.OutCirc));
        }
    }
}