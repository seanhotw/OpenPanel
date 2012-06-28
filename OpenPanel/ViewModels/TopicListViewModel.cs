using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using OpenPanel.Models;
using Newtonsoft.Json;

namespace OpenPanel.ViewModels
{
    public class TopicListViewModel : MvxViewModel
    {
        #region Properties

        private bool isSearching;
        public bool IsSearching
        {
            get { return isSearching; }
            set { isSearching = value; FirePropertyChanged("IsSearching"); }
        }

        private List<Topic> topics;
        public List<Topic> Topics
        {
            get { return topics; }
            set { topics = value; FirePropertyChanged("Topics"); }
        }

        #endregion

        public TopicListViewModel()
        {
            topics = new List<Topic>();
        }

        public void Search()
        {
            if (IsSearching) return;
            IsSearching = true;

            var topic = new Topic()
            {
                Question = "Is it a Question?",
                CreatedAt = DateTime.Now,
                CreatedBy = new User() { Name = "Tester" },
                Answers = new List<Answer>()
                {
                    new Answer() { Text = "Yes", Votes = 10 },
                    new Answer() { Text = "No", Votes = 5 }
                }
            };
            Success(new List<Topic>() { topic });
        }

		public void SelectTopic (Topic topic)
		{
			RequestNavigate<TopicDetailsViewModel>(new { topic = JsonConvert.SerializeObject(topic) });
		}

        private void Error(Exception exception)
        {
            IsSearching = false;
        }

        private void Success(IEnumerable<Topic> list)
        {
            InvokeOnMainThread(() => DisplayTopics(list));
        }

        private void DisplayTopics(IEnumerable<Topic> list)
        {
            IsSearching = false;
            Topics = list.ToList();
        }


    }
}

