using System;
using System.Threading;
using Game.Models;
using Game.Models.Tools;
using Gaze.Utilities;
using UnityEngine;

namespace Game.Components
{
    public class BaseComponent : ObservableMonoBehaviour
    {
        [SerializeField]
        protected ModelsRepositoryWrapper models;

        readonly CancellationTokenSource cancellationTokenSource = new();

        protected CancellationToken DestroyCancellationToken
            => cancellationTokenSource.Token;

        // public GameObject GameObject
        //     => this
        //             ? gameObject
        //             : null;

        // public virtual Transform Parent
        // {
        //     get
        //         => transform.parent;
        //     set
        //         => transform.SetParent(
        //             value,
        //             false);
        // }

        protected override void Awake()
        {
            base.Awake();
            models?.Load(OnModelsAssigned);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        #if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            models ??= new ModelsRepositoryWrapper(ModelsRepositoryPickerTool.GetContainerRepositoryGuid());
        }
        #endif

        public void InjectModels(
            ModelsRepository modelsRepository
        )
        {
            if (modelsRepository)
            {
                models = new ModelsRepositoryWrapper(modelsRepository);
                OnModelsAssigned();
            }
            else
            {
                ApplicationLogger.LogException(
                    new NullReferenceException(
                        $"Injecting null container repository at {gameObject}"));
            }
        }

        protected virtual void OnModelsAssigned() { }
    }
}