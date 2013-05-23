using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using MongoDB.Driver;

namespace ComicTales
{
    public class MongoDBConnector
    {
        private static readonly object Key = new object();

        public static MongoDatabase Database
        {
            get { return (MongoDatabase)HttpContext.Current.Items[Key]; }
            set { HttpContext.Current.Items[Key] = value; }
        }

        public static void Connect()
        {
            var conStr = ConfigurationManager.ConnectionStrings["ComicTalesDb"].ConnectionString;
            var databaseName = MongoUrl.Create(conStr).DatabaseName;
            Database = new MongoClient(conStr).GetServer().GetDatabase(databaseName);
        }
    }
}
