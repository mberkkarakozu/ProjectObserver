namespace GamePlay.Systems
{
    public interface IInteractable
    {
        void Interact();

        string GetInteractionPrompt();
    }
}