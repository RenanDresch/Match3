using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Logic
{
    [RequireComponent(typeof(TMP_Text))]
    public class GameTimer : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private TMP_Text counterText = default;

        private TimeSpan currentTime;

        private IEnumerator timerLabelUpdateCoroutine;

        #endregion

        #region Private Methods

        private void Reset()
        {
            counterText = GetComponent<TMP_Text>();
        }

        private void UpdateLabel()
        {
            counterText.text = $"{currentTime.Minutes:00}:{currentTime.Seconds:00}";
        }

        private IEnumerator LabelUpdateCoroutine()
        {
            while (currentTime.TotalSeconds > 1)
            {
                currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateLabel();

                var sequence = DOTween.Sequence();

                sequence.Append(counterText.transform.DOScale(1.05f, .05f)
                    .SetEase(Ease.OutCirc));

                sequence.Append(counterText.transform.DOScale(1f, .05f)
                     .SetEase(Ease.OutCirc));

                yield return new WaitForSecondsRealtime(1);
            }
        }

        #endregion

        #region Public Methods

        public Sequence ResetTimer()
        {
            currentTime = TimeSpan.FromMinutes(2);
            UpdateLabel();

            var sequence = DOTween.Sequence();

            sequence.Append(counterText.transform.DOScale(1.4f, .2f)
                .SetEase(Ease.Linear));

            sequence.Append(counterText.transform.DOScale(1f, .5f)
                .SetEase(Ease.OutBounce));

            return sequence;
        }

        public void StartTimer()
        {
            if (timerLabelUpdateCoroutine != null)
            {
                StopCoroutine(timerLabelUpdateCoroutine);
            }
            timerLabelUpdateCoroutine = LabelUpdateCoroutine();
            StartCoroutine(timerLabelUpdateCoroutine);
        }

        public void PauseTimer()
        {
            if (timerLabelUpdateCoroutine != null)
            {
                StopCoroutine(timerLabelUpdateCoroutine);
            }
        }

        #endregion
    }
}