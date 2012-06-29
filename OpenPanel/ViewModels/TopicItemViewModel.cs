using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;
using Newtonsoft.Json;
using OpenPanel.Models;

namespace OpenPanel.ViewModels
{
    public class TopicItemViewModel : BaseViewModel
    {
        private Topic topic;

        public string Question { get; set; }
        public string CreatedAt { get; set; }
        public string User { get; set; }

        public TopicItemViewModel(Topic topic)
        {
            this.topic = topic;

            Question = topic.Question;
            CreatedAt = topic.CreatedAt.ToString("ddd MM HH:mm");
            User = topic.User.Name;
        }

        public IMvxCommand ViewDetailCommand
        {
            get { return new MvxRelayCommand(() => RequestNavigate<TopicDetailsViewModel>(new { topic = JsonConvert.SerializeObject(topic) })); }
        }
    }
}
