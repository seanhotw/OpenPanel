using System;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using OpenPanel.ViewModels;

namespace OpenPanel
{
    public class AppContext : MvxApplicationObject, IMvxStartNavigation
    {
        public void Start()
        {
            RequestNavigate<TopicListViewModel>();
        }

        #region IMvxStartNavigation implementation

        public bool ApplicationCanOpenBookmarks
        {
            get { return true; }
        }

        #endregion

    }
}

