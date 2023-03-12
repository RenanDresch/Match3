using System;
using DG.Tweening;
using Gaze.MCS;
using UnityEngine;

namespace Game.Models.Novelty
{
    [Serializable]
    public class AudioContainer
    {
        [Volatile]
        public readonly IReactiveProperty<AudioSource> MusicPlayer = new ReactiveProperty<AudioSource>();
        
        [Volatile]
        public readonly IReactiveProperty<Tween> MusicTween = new ReactiveProperty<Tween>();
    }
}