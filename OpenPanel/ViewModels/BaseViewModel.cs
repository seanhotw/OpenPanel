using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ViewModels;
using OpenPanel.Services;

namespace OpenPanel.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel, IMvxServiceConsumer<IOpenPanelService>
    {
        protected IOpenPanelService OpenPanelService
        {
            get { return this.GetService<IOpenPanelService>(); }
        }
    }
}
