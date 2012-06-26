using System;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Interfaces;
using Cirrious.MvvmCross.Application;
using OpenPanel;

namespace OpenPanelApp
{
    public class Setup : MvxBaseTouchSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override MvxApplication CreateApp()
        {
            var app = new App();
            return app;
        }
    }
}

