using Game.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseButtonComponent : BaseComponent
    {
        [SerializeField]
        AudioClip onClickSound;

        [SerializeField]
        [HideInInspector]
        Button button;

        [SerializeField]
        [HideInInspector]
        AudioSource audioSource;

        SoundEffectPlayerService soundEffectPlayerService;

        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            soundEffectPlayerService = new SoundEffectPlayerService(
                this,
                models);
            button.onClick.AddListener(BaseButtonClick);
        }

        void BaseButtonClick()
        {
            soundEffectPlayerService.PlaySound(onClickSound, audioSource);
            OnButtonClick();
        }

        protected abstract void OnButtonClick();

        #if UNITY_EDITOR

        void Reset()
        {
            button = GetComponent<Button>();
            audioSource = GetComponent<AudioSource>();
        }

        #endif
    }
}