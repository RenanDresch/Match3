namespace Game.Components.Buttons
{
    public class PlayButton : BaseButtonComponent
    {
        protected override void OnButtonClick()
        {
            models.SceneContainer.SceneName.Value = models.GameStaticConfigsContainer.GameplaySceneName;
        }
    }
}