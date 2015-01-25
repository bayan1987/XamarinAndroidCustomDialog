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
using XamarinDroidCustomListView.BusinessLayer.Contracts;
using XamarinDroidCustomListView.DataAccess;

namespace XamarinDroidCustomListView.Model
{
    public class ServiceCategory : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}