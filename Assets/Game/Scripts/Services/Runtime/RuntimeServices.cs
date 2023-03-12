using Game.Models;
using Game.Services.SceneManagement;
using Gaze.Utilities;

namespace Game.Services.Runtime
{
    public class RuntimeServices : BaseService
    {
        readonly SceneSwitchService sceneSwitchService;

        public RuntimeServices(
            IDestroyable destroyable,
            ModelsRepositoryWrapper models
        ) : base(
            destroyable,
            models)
        {
            sceneSwitchService = new SceneSwitchService(
                destroyable,
                models);
        }
    }
}