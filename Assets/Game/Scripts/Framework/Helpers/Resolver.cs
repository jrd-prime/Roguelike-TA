using UnityEngine.Assertions;
using VContainer;

namespace Game.Scripts.Framework.Helpers
{
    // TODO inject container
    public static class Resolver
    {
        public static T ResolveAndCheck<T>(IObjectResolver container) where T : class
        {
            var obj = container.Resolve<T>();
            Assert.IsNotNull(obj, $"{typeof(T).Name} is null.");
            return obj;
        }
    }
}
