using Cysharp.Threading.Tasks;

namespace Game.Scripts.Framework.Bootstrap
{
    public interface ILoader
    {
        public void AddServiceToInitialize(ILoadingOperation loadingService);
        public UniTask StartServicesInitializationAsync();
    }
}