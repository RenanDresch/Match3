using UnityEngine;
using UnityEngine.UI;

namespace Game.FX
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSound : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Button button = default;
        [SerializeField]
        private AudioSource audioSource = default;

        #endregion

        #region Private Methods

        private void Awake()
        {
            button.onClick.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
        }

        private void Reset()
        {
            button = GetComponent<Button>();
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

        #endregion
    }
}