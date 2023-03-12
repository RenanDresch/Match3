using Game.Models;
using Gaze.Utilities;
using UnityEngine;

namespace Game.Services.Audio
{
    public class SoundEffectPlayerService : BaseService
    {

        public SoundEffectPlayerService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable,
            models)
        {
        }

        public void PlaySound(
            AudioClip clip,
            AudioSource source = null
        )
        {
            if (source == null)
            {
                PlayClipWithoutSource(clip);
            }
            else
            {
                source.PlayOneShot(clip);
            }
        }
        void PlayClipWithoutSource(
            AudioClip clip
        )
        {
            var gameCamera = Models.AppContainer.GameCamera.Value;
            var position = gameCamera
                ? gameCamera.transform.position
                : Vector3.zero;
            
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}