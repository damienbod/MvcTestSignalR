using System;
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

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(UnityHelpers.GetTypesWithCustomAttribute<UnityIoCPerRequestLifetimeAttribute>(AppDomain.CurrentDomain.GetAssemblies()),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    PerRequest
                                )
                     .RegisterTypes(UnityHelpers.GetTypesWithCustomAttribute<UnityIoCTransientLifetimeAttribute>(AppDomain.CurrentDomain.GetAssemblies()),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    WithLifetime.Transient
                                );

            // This method checks and for classes from Loaded Assemblies and creates a per request lifetime object for classes with the custom attribute 
            //container.RegisterTypes(AllClasses.FromLoadedAssemblies(),
            //                        UnityHelpers.FromAllInterfacesWith_PerRequestLifetimeAttribute,
            //                        WithName.Default,
            //                        PerRequest
            //                    )
            //         .RegisterType<IUnitOfWorkExample, UnitOfWorkExampleTest>(new TransientLifetimeManager());
 
        }

        public static Func<System.Type, Microsoft.Practices.Unity.LifetimeManager> PerRequest = (x) => new PerRequestLifetimeManager();
    }
}

