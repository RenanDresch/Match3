using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Models
{
    //Todo: this will pre-allocate the prefab memory and other stuff
    //This could be improved through Addressables
    [Serializable]
    public class GamePrefabsContainer
    {
        [SerializeField]
        Camera gameCameraPrefab;
        public Camera GameCameraPrefab => gameCameraPrefab;
        
        [SerializeField]
        EventSystem eventSystem;
        public EventSystem EventSystem => eventSystem;
        
        [SerializeField]
        AudioSource musicPlayerPrefab;
        public AudioSource MusicPlayerPrefab => musicPlayerPrefab;
    }
}