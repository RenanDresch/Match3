using System;
using Gaze.MCS;
using UnityEngine;

namespace Game.Models.Novelty
{
    [Serializable]
    public class NoveltyContainer
    {
        [SerializeField]
        [Container]
        AudioContainer audioContainer;
        public AudioContainer AudioContainer => audioContainer;
        
        [SerializeField]
        [Container]
        FadeContainer fadeContainer;
        public FadeContainer FadeContainer => fadeContainer;
        
        [Volatile]
        public readonly IReactiveProperty<bool> GameBoardAnimating = new ReactiveProperty<bool>();
    }
}