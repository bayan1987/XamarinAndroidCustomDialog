using System.Collections.Generic;
using System.IO;
using XamarinDroidCustomListView.Model;
using Environment = System.Environment;

namespace XamarinDroidCustomListView.DataAccess
{
	public class CategoryRepository
	{
		private ServicesDatabase _db = null;
		protected static string DbLocation;
		protected static CategoryRepository Me;

		static CategoryRepository()
		{
			Me = new CategoryRepository();
		}

		protected CategoryRepository()
		{
			//set the db location;
			DbLocation = DatabaseFilePath;

			//instantiate the database
			_db = new ServicesDatabase(DbLocation);
		}


		public static string DatabaseFilePath
		{
			get
			{
				const string sqliteFilename = "ServicesDB.db3";
				var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var path = Path.Combine(libraryPath, sqliteFilename);
				return path;
			}
		}


		// CRUD (Create, Read, Update and Delete) methods

		public static ServiceCategory GetCategory(int id)
		{
			return Me._db.GetItem<ServiceCategory>(id);
		}

		public static IEnumerable<ServiceCategory> GetCategories()
		{
			return Me._db.GetItems<ServiceCategory>();
		}

		public static int SaveCategory(ServiceCategory category)
		{
			return Me._db.SaveItem(category);
		}

		public static int DeleteCategory(int id)
		{
			return Me._db.DeleteItem<ServiceCategory>(id);
		}
	}
}