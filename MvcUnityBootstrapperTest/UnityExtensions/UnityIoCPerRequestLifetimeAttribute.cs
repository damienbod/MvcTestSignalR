using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUnityBootstrapperTest.UnityExtensions
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class UnityIoCPerRequestLifetimeAttribute : System.Attribute
    {
        public double version;

        public UnityIoCPerRequestLifetimeAttribute()
        {
            version = 1.0;
        }
    }
}
