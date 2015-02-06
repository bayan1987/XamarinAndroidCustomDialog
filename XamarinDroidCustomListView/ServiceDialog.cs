using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using XamarinDroidCustomListView.BusinessLayer.Managers;
using XamarinDroidCustomListView.Model;

namespace XamarinDroidCustomListView
{
    public class ServiceDialog : DialogFragment
    {
        //Create class properties
        protected EditText NameEditText;
        protected EditText DescriptionEditText;
        protected EditText PriceEditText;
        protected EditText AddCategoryEditText;
        protected Spinner CategorySpinner;
        protected LinearLayout CategoryLayout;
        protected CheckBox CategoryCheckBox;
        protected Button CategoryButton;

        //Create the string that will hold the value
        //Of the category drop down selected item
        protected string SelectedCategory = "";

        /// <summary>
        /// Method that creates and returns and instance of this dialog
        /// </summary>
        /// <returns></returns>
        public static ServiceDialog NewInstance()
        {
            var dialogFragment = new ServiceDialog();
            return dialogFragment;
        }

        
       
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Begin building a new dialog.
            var builder = new AlertDialog.Builder(Activity);

            //Get the layout inflater
            var inflater = Activity.LayoutInflater;

            //Inflate the layout for this dialog
            var dialogView = inflater.Inflate(Resource.Layout.AddServiceDialog, null);

            if (dialogView != null)
            {
                //Initialize the properties
                NameEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextName);
                DescriptionEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextDescription);
                PriceEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextPrice);
                AddCategoryEditText = dialogView.FindViewById<EditText>(Resource.Id.editTextAddCategory);
                CategorySpinner = dialogView.FindViewById<Spinner>(Resource.Id.spinnerCategory);
               
                CategoryLayout = dialogView.FindViewById<LinearLayout>(Resource.Id.linearLayoutCategorySection);
                //Hide this section for now
                CategoryLayout.Visibility = ViewStates.Invisible;
                CategoryCheckBox = dialogView.FindViewById<CheckBox>(Resource.Id.checkBoxAddCategory);

                CategoryCheckBox.Click += (sender, args) =>
                {
                    //If checked, show the Category section otherwise hide
                    CategoryLayout.Visibility = 
                        CategoryCheckBox.Checked ? ViewStates.Visible : ViewStates.Invisible;
                };

                CategoryButton = dialogView.FindViewById<Button>(Resource.Id.buttonCategory);
                CategoryButton.Click += (sender, args) =>
                {
                    var category = AddCategoryEditText.Text.ToString();

                    //insert new category into the database
                    if (!string.IsNullOrEmpty(category))
                    {
                        Toast.MakeText(Activity, "Please enter category name", ToastLength.Short);
                    }
                    else
                    {
                        var newCategory = new ServiceCategory {Name = category};
                        //Call the Category Manager to save the category
                        CategoryManager.SaveCategory(newCategory);
                        AddCategoryEditText.Text = "";
                        //Now call the method that loads the Spinner
                        //So the Category you just added will be an available choice
                        //in the drop down
                        LoadSpinnerData();
                    }
                };
                //populate Spinner with data from database
                //Good candidate for async
                LoadSpinnerData(); 

                //Set default selection for the spinner
                //CategorySpinner.SetSelection(0);
                //SelectedCategory = CategorySpinner.SelectedItem.ToString();
                
                //Set on item selected listener for the spinner
                CategorySpinner.ItemSelected += spinner_ItemSelected;

                builder.SetView(dialogView);
                builder.SetPositiveButton("Add Service", HandlePositiveButtonClick);
                builder.SetNegativeButton("Cancel", HandleNegativeButtonClick);
            }


            //Create the builder 
            var dialog = builder.Create();
           
            //Now return the constructed dialog to the calling activity
            return dialog;
        }

        //Handler for the drop down list
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            SelectedCategory = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
        }

        private void HandlePositiveButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog) sender;

            //Create an instance of a ServiceItem object
            var service = new ServiceItem();

            //Extact the name that was given to this Service
            var name = NameEditText.Text.ToString
                (CultureInfo.InvariantCulture).Trim();

            //Check if the name is null
            //If not assign, set the name of the Service to this name
            if (string.IsNullOrEmpty(name))
            {
                NameEditText.Error = "Service name empty";
            }
            else
            {
                service.Name = name;
            }

            //No need to check if description is null
            //This field could be optional
            service.Description = DescriptionEditText.Text.ToString
                (CultureInfo.InvariantCulture).Trim();

            var price = double.Parse(PriceEditText.Text.ToString
                (CultureInfo.InvariantCulture));
            if (price > 0)
            {
                service.Price = price;
            }
            else
            {
                PriceEditText.Error = "Set price";
            }

            //Set the category to the value of the selected item
            //From the drop down list
            service.Category = SelectedCategory;

            //Save the ServiceItem to the database and check if the
            //result is successful
            var result = ServicesManager.SaveServiceItem(service);
            if (result == 1)
            {
                ((MainActivity) this.Activity).ServiceAdded(true);
                dialog.Dismiss();
            }
            
        }

        private void HandleNegativeButtonClick(object sender, DialogClickEventArgs e)
        {
            var dialog = (AlertDialog)sender;
            dialog.Dismiss();
        }

        /// <summary>
        /// This method fetches the Categories from the database and 
        /// Populates the drop down list (Spinner) with the return value
        /// </summary>
        private void LoadSpinnerData()
        {
            var tempCategories = (List<ServiceCategory>) CategoryManager.GetCategories();
            var categories = tempCategories.Select(category => category.Name).ToList();

            var categoryAdapter = new ArrayAdapter<string>(
                Activity, Android.Resource.Layout.SimpleSpinnerItem, categories);

            categoryAdapter.SetDropDownViewResource
                (Android.Resource.Layout.SimpleSpinnerDropDownItem);
            CategorySpinner.Adapter = categoryAdapter;
        }

    }
}