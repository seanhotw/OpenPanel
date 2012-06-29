using Cirrious.MvvmCross.WindowsPhone.Views;
using OpenPanel.ViewModels;

namespace OpenPanelApp.Views
{
    public partial class TopicListPage : BaseTopicListPage
    {
        public TopicListPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarMenuItem_Click(object sender, System.EventArgs e)
        {
            ViewModel.Search();
        }
    }

    public class BaseTopicListPage : MvxPhonePage<TopicListViewModel>
    {
    }
}