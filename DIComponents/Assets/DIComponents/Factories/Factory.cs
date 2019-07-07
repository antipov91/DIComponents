using DIComponents.Core;

namespace DIComponents
{
    public class Factory<TValue> : DIFactory, IFactory<TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create()
        {
            return base.Create() as TValue;
        }
    }

    public class Factory<TParam1, TValue> : DIFactory, IFactory<TParam1, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1)
        {
            return base.Create(param1) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TValue> : DIFactory, IFactory<TParam1, TParam2, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2)
        {
            return base.Create(param1, param2) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return base.Create(param1, param2, param3) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TParam4, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TParam4, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            return base.Create(param1, param2, param3, param4) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            return base.Create(param1, param2, param3, param4, param5) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            return base.Create(param1, param2, param3, param4, param5, param6) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            return base.Create(param1, param2, param3, param4, param5, param6, param7) as TValue;
        }
    }

    public class Factory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> : DIFactory, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> where TValue : class
    {
        public Factory(IObjectActivator objectActivator) : base(objectActivator) { }

        public TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
        {
            return base.Create(param1, param2, param3, param4, param5, param6, param7, param8) as TValue;
        }
    }
}