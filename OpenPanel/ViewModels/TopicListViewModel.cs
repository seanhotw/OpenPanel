using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.ViewModels;
using OpenPanel.Models;
using Newtonsoft.Json;
using OpenPanel.Services;


namespace OpenPanel.ViewModels
{
    public class TopicListViewModel : MvxViewModel, IMvxServiceConsumer<IOpenPanelService>
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

            OpenPanelService.GetTopicsAsync(Success, Error);
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

        private IOpenPanelService OpenPanelService
        {
            get { return this.GetService<IOpenPanelService>(); }
        }
    }
}

