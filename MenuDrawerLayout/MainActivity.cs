﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;

namespace MenuDrawerLayout
{
    [Activity(Label = "MenuDrawerLayout", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {

        private SupportToolbar mtoolbar;
        private MyToolbarDrawerToggle mDrawerToggle;
        private DrawerLayout mDrawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mtoolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            SetSupportActionBar(mtoolbar);
            
            mDrawerToggle = new MyToolbarDrawerToggle(
                    this,
                    mDrawerLayout,
                    Resource.String.Open,
                    Resource.String.Closed
                );

            mDrawerLayout.AddDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            mDrawerToggle.SyncState();

            if(bundle != null)
            {
                if(bundle.GetString("DrawerState") == "Open")
                {
                    SupportActionBar.SetTitle(Resource.String.Open);
                }
                else
                {
                    SupportActionBar.SetTitle(Resource.String.Closed);
                }

            }else
            {
                SupportActionBar.SetTitle(Resource.String.Closed);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(item.ItemId == global::Android.Resource.Id.Home)
            {
                mDrawerToggle.OnOptionsItemSelected(item);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Open");
            }else
            {
                outState.PutString("DraweState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }
    }
}

