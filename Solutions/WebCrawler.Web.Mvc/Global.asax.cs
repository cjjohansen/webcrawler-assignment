﻿

namespace WebCrawler.Web.Mvc
{
    using System;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Reflection;
    using Castle.Windsor;
        // WebCrawler.Web.Mvc.CastleWindsor
    using CastleWindsor;

    using CommonServiceLocator.WindsorAdapter;

    using Controllers;

    using Infrastructure.NHibernateMaps;

    using SharpArch.Domain.Events;

    using log4net.Config;

    using Microsoft.Practices.ServiceLocation;

    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Web.Mvc;
    using SharpArch.Web.Mvc.Castle;
    using SharpArch.Web.Mvc.ModelBinder;
    

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private WebSessionStorage webSessionStorage;

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization must occur in Init().
        /// But Init() may be invoked more than once; accordingly, we introduce a thread-safe
        /// mechanism to ensure it's only initialized once.
        /// See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.webSessionStorage = new WebSessionStorage(this);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(this.InitialiseNHibernateSessions);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            Exception ex = this.Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }


        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            ViewEngines.Engines.Clear();

            ViewEngines.Engines.Add(new RazorViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();

            ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());

            this.InitializeServiceLocator();

            AreaRegistration.RegisterAllAreas();
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);


           

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            ComponentRegistrar.AddComponentsTo(container);
            container.RegisterControllers(typeof(HomeController).Assembly);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

           
            

            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);
        }

        private void InitialiseNHibernateSessions()
        {
            NHibernateSession.ConfigurationCache = new NHibernateConfigurationFileCache();

            NHibernateSession.Init(
                this.webSessionStorage,
                new[] { Server.MapPath("~/bin/WebCrawler.Infrastructure.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                Server.MapPath("~/NHibernate.config"));
        }

    }
}