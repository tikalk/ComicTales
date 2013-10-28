using System.Configuration;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Unity.Mvc4;
using ComicTales.Entities;

namespace ComicTales
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();  
            RegisterMongoDb(container);

            return container;
        }

        private static void RegisterMongoDb(IUnityContainer container)
        {
            //register the database
            var conStr = ConfigurationManager.ConnectionStrings["ComicTalesDb"].ConnectionString;
            var databaseName = MongoUrl.Create(conStr).DatabaseName;
            var database = new MongoClient(conStr).GetServer().GetDatabase(databaseName);
            container.RegisterInstance(typeof(MongoDatabase), database);

            //register the repositroy
            container.RegisterInstance(typeof (MongoDBRepository), new MongoDBRepository(database));

            //register the types
            BsonClassMap.RegisterClassMap<ComicStory>();
            BsonClassMap.RegisterClassMap<ComicTile>();
            BsonClassMap.RegisterClassMap<TileOverlay>();
        }

    }
}