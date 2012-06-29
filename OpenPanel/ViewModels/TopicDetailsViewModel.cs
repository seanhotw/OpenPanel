using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OpenPanel.Models;
using SignalR.Client;
using SignalR.Client.Hubs;

namespace OpenPanel.ViewModels
{
    public class TopicDetailsViewModel : BaseViewModel
    {
        #region Properties

		private Topic topic;
        public Topic Topic
        {
            get { return topic; }
            set { topic = value; FirePropertyChanged("Topic"); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; FirePropertyChanged("Title"); }
        }

        private List<AnswerItemViewModel> answers;
        public List<AnswerItemViewModel> Answers
        {
            get { return answers; }
            set { answers = value; FirePropertyChanged("Answers"); }
        }

        #endregion

        private HubConnection hubConnection;

		public TopicDetailsViewModel (string topic)
		{
			Topic = JsonConvert.DeserializeObject<Topic>(topic);
            Title = Topic.Question;
            Answers = Topic.Answers.Select(a => new AnswerItemViewModel(a)).ToList();

            StartHubConnection();
		}

        private void StartHubConnection()
        {
            hubConnection = new HubConnection("http://openpanel.apphb.com");
            var topicHub = hubConnection.CreateProxy("topicHub");

            topicHub.On<int, int, int>("voteUpdated", (answerId, topicId, vote) =>
            {
                if (Topic.Id == topicId)
                {
                    InvokeOnMainThread(() => { UpdateVoteCount(answerId, vote); });
                }
            });

            hubConnection.Start();
        }

        protected override void Close()
        {
            hubConnection.Stop();

            base.Close();
        }

        private void UpdateVoteCount(int answerId, int vote)
        {
            var answerItem = Answers.FirstOrDefault(a => a.Id == answerId);
            if (answerItem != null)
            {
                answerItem.Update(vote);
            }
        }
	}
}

