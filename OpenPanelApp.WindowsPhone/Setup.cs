using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using Microsoft.Phone.Controls;

namespace OpenPanelApp
{
    public class Setup : MvxBaseWindowsPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override MvxApplication CreateApp()
        {
            var app = new OpenPanel.App();
            return app;
        }
    }
}
