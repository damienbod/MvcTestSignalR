using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcUnityBootstrapperTest.Logging;
using MvcUnityBootstrapperTest.UnityExtensions;

namespace MvcUnityBootstrapperTest.Business
{
    [UnityIoCTransientLifetimeAttribute]
    public class UnitOfWorkExample : IUnitOfWorkExample
    {
        static int _counter = 0; 
        public UnitOfWorkExample()
        {
            _counter++;
            UnityEventLogger.Log.CreateUnityMessage("UnitOfWorkExampleTest " + _counter );
        }

        private bool _disposed = false;

        public void Dispose()
        {
            UnityEventLogger.Log.LogUnityMessage("Entered Dispose" + _counter);
            if (!_disposed)
            {
                _counter--;
                UnityEventLogger.Log.DisposeUnityMessage("UnitOfWorkExampleTest " + _counter);
                _disposed = true;
            }
        }

        public string HelloFromUnitOfWorkExample()
        {
            UnityEventLogger.Log.LogUnityMessage("Hello UnitOfWorkExampleTest " + _counter);
            return "HelloFromUnitOfWorkExample";
        }
    }
}