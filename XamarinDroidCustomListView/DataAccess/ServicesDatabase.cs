using System.Collections.Generic;
using System.Linq;
using Android.Util;
using XamarinDroidCustomListView.BusinessLayer.Contracts;
using XamarinDroidCustomListView.Model;

namespace XamarinDroidCustomListView.DataAccess
{
    public class ServicesDatabase : SQLiteConnection
    {
        
        static readonly object Locker = new object();

        /// <summary>
        /// Initialize a new instance of the ServiceDatabase
        /// </summary>
        /// <param name="path"></param>
        public ServicesDatabase(string path) : base(path)
        {
            //Create the tables
            CreateTable<ServiceItem>();
            CreateTable<ServiceCategory>();
            Log.Info(MainActivity.Tag, "Databases Created");

        }

        //Get all items in the database method
        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }

        //Get specific item in the database method with Id
        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    Update(item);
                    return item.Id;
                }
                return Insert(item);
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return Delete<T>(new T() {Id = id});
            }
        }

    }
}