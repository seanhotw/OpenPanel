using System;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using OpenPanel.Services;

namespace OpenPanel
{
    public class App : MvxApplication, IMvxServiceProducer<IMvxStartNavigation>, IMvxServiceProducer<IOpenPanelService>
    {
        public App()
        {
            InitializeAppContext();
            InitializeOpenPanelService();
        }

        private void InitializeAppContext()
        {
            this.RegisterServiceInstance<IMvxStartNavigation>(new AppContext());
        }

        private void InitializeOpenPanelService()
        {
            this.RegisterServiceInstance<IOpenPanelService>(new OpenPanelService());
        }
    }
}

