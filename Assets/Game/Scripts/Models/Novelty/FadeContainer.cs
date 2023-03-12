using System;
using DG.Tweening;
using Gaze.MCS;

namespace Game.Models.Novelty
{
    [Serializable]
    public class FadeContainer
    {
        [Volatile]
        public readonly IReactiveProperty<Tween> FadeTween = new ReactiveProperty<Tween>();
        [Volatile]
        public readonly IReactiveProperty<float> Fade = new ReactiveProperty<float>(1);
        [Volatile]
        public readonly IReactiveProperty<bool> FadingOut = new ReactiveProperty<bool>(true);
    }
}