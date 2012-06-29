using System;
using Cirrious.MvvmCross.Commands;
using Cirrious.MvvmCross.Interfaces.Commands;
using OpenPanel.Models;

namespace OpenPanel.ViewModels
{
    public class AnswerItemViewModel : BaseViewModel
    {
        #region Properties

        public int Id { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; FirePropertyChanged("Text"); }
        }

        private int vote;
        public int Vote
        {
            get { return vote; }
            set { vote = value; FirePropertyChanged("Vote"); }
        }

        private bool isVoting;
        public bool IsVoting
        {
            get { return isVoting; }
            set { isVoting = value; FirePropertyChanged("IsVoting"); }
        }

        #endregion

        #region Commands

        public IMvxCommand VoteCommand
        {
            get
            {
                return new MvxRelayCommand(() =>
                {
                    DoVote();
                });
            }
        }

        #endregion

        public AnswerItemViewModel(Answer answer)
        {
            Id = answer.Id;
            Text = answer.Text;
            Vote = answer.Vote;
        }

        public void DoVote()
        {
            OpenPanelService.VoteAsync(Id, Success, Error);
        }

        public void Update(int vote)
        {
            Vote = vote;
        }

        private void Error(Exception exception)
        {
            InvokeOnMainThread(() => { IsVoting = false; });
        }

        private void Success()
        {
            InvokeOnMainThread(() => { IsVoting = false; });
        }
    }
}
