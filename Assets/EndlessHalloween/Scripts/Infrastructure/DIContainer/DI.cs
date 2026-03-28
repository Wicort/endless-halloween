namespace DIContainer
{
    public class DI
    {
        private static DI _container;

        public static DI Container => _container ?? (_container = new DI());

        public void RegisterSingle<TService>(TService implementation) where TService : IBean
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IBean
        {
            return Implementation<TService>.ServiceInstance;
        }

        private static class Implementation<TService> where TService : IBean
        {
            public static TService ServiceInstance;
        }
    }
}
