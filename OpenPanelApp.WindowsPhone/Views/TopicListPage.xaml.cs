using Cirrious.MvvmCross.WindowsPhone.Views;
using OpenPanel.ViewModels;
using System.Windows.Navigation;

namespace OpenPanelApp.Views
{
    public partial class TopicListPage : BaseTopicListPage
    {
        public TopicListPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.Search();
        }
    }

    public class BaseTopicListPage : MvxPhonePage<TopicListViewModel>
    {
    }
}