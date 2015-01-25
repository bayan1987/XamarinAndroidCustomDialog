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

namespace XamarinDroidCustomListView
{
    public class ServiceDialog : DialogFragment
    {
        protected EditText NameEditText;
        protected EditText DescriptionEditText;
        protected EditText PriceEditText;
        protected EditText AddCategoryEditText;
        protected Spinner CategorySpinner;
        protected LinearLayout CategoryLayout;
        protected CheckBox CategoryCheckBox;
        protected Button CategoryButton;

        public static ServiceDialog NewInstance()
        {
            var dialogFragment = new ServiceDialog();
            return dialogFragment;
        }

       
        public override void OnCreate(Bundle savedInstanceState)
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
                    var category = AddCategoryEditText.Text.ToString(CultureInfo.InvariantCulture);
                };
            }
        }
    }
}