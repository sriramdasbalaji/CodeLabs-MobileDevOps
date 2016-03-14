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
using MyHealth.Client.Core.Services;
using MyHealth.Client.Core;
using MyHealth.Client.Droid.Views;
using HockeyApp;

namespace MyHealth.Client.Droid.Services
{
    public class HockeyAppFeedbackService : IHockeyAppFeedbackService
    {
        public void LaunchHockeyAppFeedback()
        {
            FeedbackManager.Register(MainActivity.CurrentActivity, AppSettings.HockeyAppID, null);
            FeedbackManager.ShowFeedbackActivity(MainActivity.CurrentActivity);
        }
    }
}