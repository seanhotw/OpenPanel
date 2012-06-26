using System;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;

namespace OpenPanel
{
    public class App : MvxApplication, IMvxServiceProducer<IMvxStartNavigation>
    {
        public App()
        {
            InitializeAppContext();
        }

        private void InitializeAppContext()
        {
            this.RegisterServiceInstance<IMvxStartNavigation>(new AppContext());
        }
    }
}

