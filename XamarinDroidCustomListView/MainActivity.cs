using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using XamarinDroidCustomListView.BusinessLayer.Managers;
using XamarinDroidCustomListView.Model;


namespace XamarinDroidCustomListView
{
    [Activity(Label = "Xamarin Droid Custom ListView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected ListView MyServiceListView;
        protected List<ServiceItem> MyServiceItems;
        protected ServiceItemsAdapter MyServiceItemsAdapter;
        public static readonly string Tag = typeof (MainActivity).ToString();
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.Main);
            MyServiceListView = FindViewById<ListView>(Resource.Id.service_list);

            //Create an empty list of Service List
            MyServiceItems = (List<ServiceItem>) ServicesManager.GetServiceItems();
            MyServiceItemsAdapter = new ServiceItemsAdapter(this, MyServiceItems);

            MyServiceListView.Adapter = MyServiceItemsAdapter;
            var emptyText = FindViewById<TextView>(Resource.Id.service_list_empty);
            MyServiceListView.EmptyView = emptyText;

        }

        protected override void OnResume()
        {
            base.OnResume();
           // LoadData();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.ServiceMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var id = item.ItemId;
            if (id == Resource.Id.action_add_services)
            {
                var dialog = ServiceDialog.NewInstance();
                dialog.Show(FragmentManager, "dialog");
            }
            return base.OnOptionsItemSelected(item);
        }


        private void LoadData()
        {
            //First clear the adapter of any Services it has 
           // MyServiceItemsAdapter.NotifyDataSetInvalidated();

            Log.Info("LoadData", "Creating Service Instance");
            var service1 = new ServiceItem
            {
                Name = "Baby Sitting Service",
                Description = "Caring baby sitting service",
                Price = 75,
                Category = "Domestic"
                
            };
            //MyServiceItemsAdapter.Add(service1);
           var result = ServicesManager.SaveServiceItem(service1);
            Log.Info(Tag," First insert " + result);


            var service2 = new ServiceItem
            {
                Name = "House painting",
                Description = "Great looking paint service",
                Price = 150,
                Category = "Labor"

            };
           //MyServiceItemsAdapter.Add(service2);
           var result2 = ServicesManager.SaveServiceItem(service2);
           Log.Info(Tag, " First insert " + result2);
        }
    }

    
}

