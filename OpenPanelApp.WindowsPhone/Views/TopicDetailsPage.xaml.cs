using Cirrious.MvvmCross.WindowsPhone.Views;
using OpenPanel.ViewModels;

namespace OpenPanelApp.Views
{
    public partial class TopicDetailsPage : BaseTopicDetailsPage
    {
        public TopicDetailsPage()
        {
            InitializeComponent();
        }
    }

    public class BaseTopicDetailsPage : MvxPhonePage<TopicDetailsViewModel>
    {
    }
}