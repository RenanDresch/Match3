using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game.FX
{
    public class GeneralAnimator : MonoBehaviour
    {
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

        public Sequence AnimateGemDrop(Dictionary<Transform, Vector3> newGemPositions)
        {
            var sequence = DOTween.Sequence();

            foreach (var gemPosition in newGemPositions)
            {
                sequence.Join(gemPosition.Key.DOMove(
                    gemPosition.Value,
                    0.2f));
            }

            return sequence;
        }
    }
}