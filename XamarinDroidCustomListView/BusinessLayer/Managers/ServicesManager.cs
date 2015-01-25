using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    public static class ServicesManager
    {
        static ServicesManager()
        {
            
        }

        public static ServiceItem GetServiceItem(int id)
        {
            return ServicesRepository.GetServiceItem(id);
        }

        public static IList<ServiceItem> GetServiceItems()
        {
            return new List<ServiceItem>(ServicesRepository.GetServiceItems());
        }

        public static int SaveServiceItem(ServiceItem item)
        {
            return ServicesRepository.SaveServiceItem(item);
        }

        public static int DeleteServiceItem(int id)
        {
            return ServicesRepository.DeleteServiceItem(id);
        }
    }
}