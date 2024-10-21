namespace Game.Scripts.Framework.Bootstrap
{
    public interface ILoadingOperation
    {
        string Description { get; }
        public void LoaderServiceInitialization();
    }
}
