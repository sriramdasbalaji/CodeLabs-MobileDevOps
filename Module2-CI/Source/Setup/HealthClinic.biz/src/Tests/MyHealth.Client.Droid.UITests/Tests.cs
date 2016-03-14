using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;

namespace MyHealth.Client.Droid.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("Home screen");
            //app.Repl();
        }

        [Test]
        public void Test_SwitchBetweenMeds()
        {
            app.Screenshot("Med1");
            app.WaitForElement(q => q.Marked("toolbar_title").Text("Home"), "Home screen too slow", TimeSpan.FromMinutes(2));
            app.Flash(q => q.Marked("countdown_title").Text("Tylenol 100ml"));

            app.Tap(q => q.Button("medicine2Button"));
            app.Screenshot("Med2");
            app.Flash(q => q.Marked("countdown_title").Text("Tamiflu 100ml"));

            app.Tap(q => q.Button("medicine1Button"));
            app.Screenshot("Med1-again");
            app.Flash(q => q.Marked("countdown_title").Text("Tylenol 100ml"));
        }
    }
}

