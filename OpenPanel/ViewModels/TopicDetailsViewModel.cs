using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using OpenPanel.Models;
using Newtonsoft.Json;

namespace OpenPanel.ViewModels
{
    public class TopicDetailsViewModel : MvxViewModel
    {
        #region Properties

		private Topic topic;
        public Topic Topic
        {
            get { return topic; }
            set { topic = value; FirePropertyChanged("Topic"); }
        }

        #endregion

		public TopicDetailsViewModel (string topic)
		{
			Topic = JsonConvert.DeserializeObject<Topic>(topic);
		}

		public void Vote (Answer answer)
		{
			Answer result = topic.Answers.Find(delegate(Answer ans)
			{
				return ans.Text == answer.Text;
			});
			if (result != null) {
				result.Votes++;
				Topic = topic;
			}
		}
	}
}

