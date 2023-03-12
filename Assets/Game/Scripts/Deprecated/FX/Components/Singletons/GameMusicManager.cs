using DG.Tweening;
using Game.Contracts;
using UnityEngine;

namespace Game.FX
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioListener))]
    [DefaultExecutionOrder(-100)]
    public class GameMusicManager : MonoBehaviourSingleton<GameMusicManager>
    {
        #region Fields

        [SerializeField]
        private AudioSource musicSource = default;

        #endregion

        #region Private Methods

        private void OnEnable()
        {
            Instance = this;
            musicSource.volume = 0;
            musicSource.loop = true;
            musicSource.Play();

            musicSource.DOFade(1, 2);
        }

        private void Reset()
        {
            musicSource = GetComponent<AudioSource>();
        }

        #endregion

        #region Public Methods

        public Tween SetMusicVolume(float volume, float time = 2)
        {
            return musicSource.DOFade(volume, time);
        }

        #endregion
    }
}