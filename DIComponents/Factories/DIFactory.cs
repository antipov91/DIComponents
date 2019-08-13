namespace DIComponents.Core
{
    public abstract class DIFactory : IFactory
    {
        protected IObjectActivator objectActivator;

        private DIContainer container;

        public DIFactory(IObjectActivator objectActivator)
        {
            this.objectActivator = objectActivator;
        }

        protected object Create(params object[] args)
        {
            var obj = objectActivator.Create(args);
            DIComponentsInitializer.Inject(obj);
            return obj;
        }
    }
}