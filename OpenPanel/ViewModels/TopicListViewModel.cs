using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;
using Newtonsoft.Json;
using OpenPanel.Models;
using OpenPanel.Services;

namespace OpenPanel.ViewModels
{
    public class TopicListViewModel : BaseViewModel
    {
        #region Properties

        private bool isSearching;
        public bool IsSearching
        {
            get { return isSearching; }
            set { isSearching = value; FirePropertyChanged("IsSearching"); }
        }

        private List<TopicItemViewModel> topics;
        public List<TopicItemViewModel> Topics
        {
            get { return topics; }
            set { topics = value; FirePropertyChanged("Topics"); }
        }

        #endregion

        #region Commands

        public IMvxCommand SearchCommand
        {
            get
            {
                return new MvxRelayCommand(() =>
                {
                    Search();
                });
            }
        }

        #endregion

        public TopicListViewModel()
        {
            topics = new List<TopicItemViewModel>();

            Search();
        }

        public void Search()
        {
            if (IsSearching) return;
            IsSearching = true;

            OpenPanelService.GetTopicsAsync(Success, Error);
        }

		public void SelectTopic (Topic topic)
		{
			RequestNavigate<TopicDetailsViewModel>(new { topic = JsonConvert.SerializeObject(topic) });
		}

        private void Error(Exception exception)
        {
            InvokeOnMainThread(() => { IsSearching = false; });
        }

        private void Success(IEnumerable<Topic> list)
        {
            var topicViewModels = list.Select(t => new TopicItemViewModel(t)).ToList();
            InvokeOnMainThread(() => DisplayTopics(topicViewModels));
        }

        private void DisplayTopics(List<TopicItemViewModel> topicViewModels)
        {
            IsSearching = false;
            Topics = topicViewModels;
        }
    }
}

