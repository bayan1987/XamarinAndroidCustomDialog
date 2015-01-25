using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinDroidCustomListView.DataAccess;
using XamarinDroidCustomListView.Model;

namespace XamarinDroidCustomListView.BusinessLayer.Managers
{
    public class CategoryManager
    {
         static CategoryManager()
        {
            
        }

        public static ServiceCategory GetCategory(int id)
        {
            return CategoryRepository.GetCategory(id);
        }

        public static IList<ServiceCategory> GetCategories()
        {
            return new List<ServiceCategory>(CategoryRepository.GetCategories());
        }

        public static int SaveCategory(ServiceCategory category)
        {
            return CategoryRepository.SaveCategory(category);
        }

        public static int DeleteCategory(int id)
        {
            return CategoryRepository.DeleteCategory(id);
       
    }
}