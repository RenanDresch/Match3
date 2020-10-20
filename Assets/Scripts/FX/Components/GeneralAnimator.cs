using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FX
{
    public class GeneralAnimator : MonoBehaviour
    {
        #region Gem Animations

        public Tween AnimateDelection(Transform gem)
        {
            return gem.DOScale(1.4f, .5f).SetEase(Ease.OutBack);
        }

        public Tween AnimateSelection(Transform gem)
        {
            return gem.DOScale(1.4f, .5f).SetEase(Ease.OutBack);
        }

        public Tween AnimateDeselection(Transform gem)
        {
            return gem.DOScale(1, .5f).SetEase(Ease.OutBack);  
        }

        public Sequence AnimateSwap(Transform gemA, Transform gemB)
        {
            var sequence = DOTween.Sequence();

            var gemAPosition = gemA.position;
            sequence.Append(gemA.DOMove(gemB.transform.position, .2f));
            sequence.Join(gemB.DOMove(gemAPosition, .2f));

            return sequence;
        }

        public Sequence AnimateFailedSwap(Transform gemA, Transform gemB)
        {
            var sequence = DOTween.Sequence();

            var gemAPosition = gemA.position;
            var gemBPosition = gemB.position;

            sequence.Append(gemA.DOMove(gemBPosition, .2f));
            sequence.Join(gemB.DOMove(gemAPosition, .2f));

            sequence.Append(gemA.DOMove(gemAPosition, .2f));
            sequence.Join(gemB.DOMove(gemBPosition, .2f));

            return sequence;
        }

        public Sequence AnimateGemFade(SpriteRenderer[] renderers)
        {
            var sequence = DOTween.Sequence();

            foreach (var spriteRenderer in renderers)
            {
                SpriteRenderer sr = new SpriteRenderer();

                sequence.Join(spriteRenderer.transform.DOScale(
                    1.5f,
                    0.2f));

                sequence.Join(DOTween.To(() => spriteRenderer.color,
                    (x) => spriteRenderer.color = x,
                    Color.clear, 0.2f));
            }

            return sequence;
        }

        public Sequence AnimateGemSpawn(SpriteRenderer[] renderers)
        {
            var sequence = DOTween.Sequence();

            foreach (var spriteRenderer in renderers)
            {
                SpriteRenderer sr = new SpriteRenderer();

                sequence.Join(spriteRenderer.transform.DOScale(
                    1f,
                    0.2f));

                sequence.Join(DOTween.To(() => spriteRenderer.color,
                    (x) => spriteRenderer.color = x,
                    Color.white, 0.2f));
            }

            return sequence;
        }

        public Sequence AnimateGemMove(Dictionary<Transform, Vector3> newGemPositions)
        {
            var sequence = DOTween.Sequence();

            foreach (var gemPosition in newGemPositions)
            {
                sequence.Join(gemPosition.Key.DOMove(
                    gemPosition.Value,
                    0.35f));
                sequence.Join(gemPosition.Key.DOScale(
                    Vector3.one,
                    0.35f));
            }

            return sequence;
        }


        #endregion

        #region Text Animations

        public Sequence BounceText(Transform textTransform)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(textTransform.DOScale(1.4f, .2f)
                .SetEase(Ease.Linear));

            sequence.Append(textTransform.DOScale(1f, .5f)
                .SetEase(Ease.OutBounce));

            return sequence;
        }

        public Sequence PunchText(Transform textTransform)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(textTransform.DOScale(1.05f, .05f)
                .SetEase(Ease.OutCirc));

            sequence.Append(textTransform.DOScale(1f, .05f)
                 .SetEase(Ease.OutCirc));

            return sequence;
        }

        #endregion

        #region Canvas Group Animations

        public Tween FadeCanvasIn(CanvasGroup cg)
        {
            return cg.DOFade(1, .5f);
        }

        public Tween FadeCanvasOut(CanvasGroup cg)
        {
            return cg.DOFade(0, .5f);
        }

        #endregion
    }
}