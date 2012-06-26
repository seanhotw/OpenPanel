using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using OpenPanel.Models;

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
                Title = "",
                CreatedAt = DateTime.Now,
                CreatedBy = new User() { Name = "Tester" },
                Opinions = new List<Opinion>()
                {
                    new Opinion() { Text = "Yes", Votes = 10 },
                    new Opinion() { Text = "No", Votes = 5 }
                }
            };
            Success(new List<Topic>() { topic });
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

