using Game.Services.Novelty;

namespace Game.Components.Novelty
{
    public class MusicController : BaseComponent
    {
        MusicService musicService;
        
        protected override void OnModelsAssigned()
        {
            base.OnModelsAssigned();
            musicService = new MusicService(
                this,
                models);
        }
    }
}