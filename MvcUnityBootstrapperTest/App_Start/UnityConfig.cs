using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using MvcUnityBootstrapperTest.Business;
using MvcUnityBootstrapperTest.UnityExtensions;
using System.Collections.Generic;

namespace MvcUnityBootstrapperTest.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        private const string ClassesToScan = "MvcUnityBootstrapperTest";

        public static void RegisterTypes(IUnityContainer container)
        {
            var myAssemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith(ClassesToScan))
                    .ToArray();

            container.RegisterTypes(UnityHelpers.GetTypesWithCustomAttribute<UnityIoCPerRequestLifetimeAttribute>(myAssemblies),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    PerRequest
                                )
                     .RegisterTypes(UnityHelpers.GetTypesWithCustomAttribute<UnityIoCTransientLifetimeAttribute>(myAssemblies),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    WithLifetime.Transient
                                );
        }

        public static Func<System.Type, Microsoft.Practices.Unity.LifetimeManager> PerRequest = (x) => new PerRequestLifetimeManager();
    }
}

