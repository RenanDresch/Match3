using System.Threading;
using Game.Models;
using Gaze.MCS;
using Gaze.Utilities;

namespace Game.Services
{
    public abstract class BaseService : Service
    {
        readonly IDestroyable destroyable;
        protected readonly ModelsRepositoryWrapper Models;
        readonly CancellationTokenSource serviceCancellationTokenSource = new();

        protected BaseService(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(destroyable)
        {
            this.destroyable = destroyable;
            destroyable.OnDestroyEvent += Shutdown;
            Models = models;
        }

        protected CancellationToken ServiceCancellationToken => serviceCancellationTokenSource.Token;

        public virtual void Shutdown()
        {
            destroyable.OnDestroyEvent -= Shutdown;
            serviceCancellationTokenSource.Cancel();
        }
    }
}