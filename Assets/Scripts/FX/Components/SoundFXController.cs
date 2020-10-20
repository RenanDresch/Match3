using UnityEngine;

namespace Game.FX
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundFXController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private AudioSource audioSource = default;

        [SerializeField]
        private AudioClip selectClip = default;

        [SerializeField]
        private AudioClip swapClip = default;

        [SerializeField]
        private AudioClip clearClip = default;

        #endregion

        #region Private Methods

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        #endregion

        #region Public Methods

        public void PlaySelection()
        {
            audioSource.PlayOneShot(selectClip);
        }

        public void PlaySwap()
        {
            audioSource.PlayOneShot(swapClip);
        }

        public void PlayClear()
        {
            audioSource.PlayOneShot(clearClip);
        }

        #endregion
    }
}