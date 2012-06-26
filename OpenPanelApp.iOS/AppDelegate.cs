using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;

namespace OpenPanelApp
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate, IMvxServiceConsumer<IMvxStartNavigation>
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
			
            var presenter = new MvxTouchViewPresenter(this, window);

            var setup = new Setup(this, presenter);
            setup.Initialize();

            var start = this.GetService<IMvxStartNavigation>();
            start.Start();

            window.MakeKeyAndVisible();
			
            return true;
        }

        public static bool IsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public static bool IsPad
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad; }
        }

        public static bool HasRetina
        {
            get
            {
                if (UIScreen.MainScreen.RespondsToSelector(new Selector("scale")))
                {
                    return UIScreen.MainScreen.Scale == 2.0;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

