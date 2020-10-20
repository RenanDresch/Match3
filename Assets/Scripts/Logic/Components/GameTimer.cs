using DG.Tweening;
using Game.FX;
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

        [SerializeField]
        private GeneralAnimator animator = default;

        private TimeSpan currentTime;

        private IEnumerator timerLabelUpdateCoroutine;

        #endregion

        #region Properties

        public System.Action OnTimeUp { get; set; }

        #endregion

        #region Private Methods

        private void OnDestroy()
        {
            OnTimeUp = null;
        }

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

                animator.PunchText(counterText.transform);

                yield return new WaitForSecondsRealtime(1);
            }

            currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));
            UpdateLabel();

            animator.PunchText(counterText.transform);

            OnTimeUp?.Invoke();
        }

        #endregion

        #region Public Methods

        public Sequence ResetTimer()
        {
            currentTime = TimeSpan.FromSeconds(GameSettingsManager.Instance.TimerResetSeconds);
            UpdateLabel();

            return animator.BounceText(counterText.transform);
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