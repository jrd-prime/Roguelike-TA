using Cysharp.Threading.Tasks;

namespace Game.Scripts.Framework.Bootstrap
{
    public interface ILoader
    {
        public void AddServiceForInitialization(ILoadingOperation loadingService);
        public UniTask StartServicesInitializationAsync();
    }
}