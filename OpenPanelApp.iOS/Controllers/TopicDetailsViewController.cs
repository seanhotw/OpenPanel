using System;
using System.Collections.Generic;
using System.Drawing;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using OpenPanel.Models;
using OpenPanel.ViewModels;

namespace OpenPanelApp
{
	public class TopicDetailsViewController : MvxTouchViewController<TopicDetailsViewModel>
	{
		public UITableView TableView { get; set; }

		public TopicDetailsViewController(MvxShowViewModelRequest request) : base(request)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = ViewModel.Topic.User.Name;

			TableView = new UITableView (RectangleF.Empty, UITableViewStyle.Grouped)
            {
				Frame = this.ContentFrame(),
                Source = new TableSource (View, ViewModel.Topic, ViewModel.Answers)
			};
			View.AddSubview(TableView);

            ViewModel.Answers.ForEach(answer => {
                answer.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Vote")
                    {
                        TableView.ReloadData();
                    }
                };
            });
		}

		class TableSource : UITableViewSource
		{
			private const string CreatorCellId = "CreatorCellId";
			private const string QuestionCellId = "QuestionCellId";
			private const string AnswerCellId = "AnswerCellId";

            public UIView RootView { get; set; }
            public Topic Topic { get; set; }
            public List<AnswerItemViewModel> Answers { get; set; }

			private const int NbOfSections = 3;
			private const int CreatorSection = 0;
			private const int QuestionSection = 1;
			private const int AnswersSection = 2;

			public TableSource(UIView rootView, Topic topic, List<AnswerItemViewModel> answers)
			{
                RootView = rootView;
                Topic = topic;
                Answers = answers;
			}

			public override int NumberOfSections(UITableView tableView)
			{
				return NbOfSections;
			}

			public override string TitleForHeader(UITableView tableView, int section)
			{
                return (section == AnswersSection) ? "Answers" : String.Empty;
			}

			public override int RowsInSection(UITableView tableview, int section)
			{
				int nbOfRows = 0;
				switch (section) {
				case CreatorSection:
					// user name
					// created at
					nbOfRows = 2;
					break;
				case QuestionSection:
					// question
					nbOfRows = 1;
					break;
				case AnswersSection:
					// nb of answers
					nbOfRows = Answers.Count;
					break;
				}

				return nbOfRows;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = null;
				switch (indexPath.Section) {
				case CreatorSection:
					cell = MakeCell (tableView, CreatorCellId);
					switch (indexPath.Row) {
					case 0:
						cell.TextLabel.Text = "Name";
						cell.DetailTextLabel.Text = Topic.User.Name;
						break;
					case 1:
						cell.TextLabel.Text = "Created at";
						cell.DetailTextLabel.Text = Topic.CreatedAt.ToString();
						break;
					}
					break;
				case QuestionSection:
					cell = MakeCell (tableView, CreatorCellId);
					cell.TextLabel.Text = "Question";
					cell.DetailTextLabel.Text = Topic.Question;
					break;
				case AnswersSection:
					cell = MakeCellForAnswer (tableView, CreatorCellId);
					cell.TextLabel.Text = Answers[indexPath.Row].Text;
					cell.DetailTextLabel.Text = Answers[indexPath.Row].Vote.ToString();
					break;
				}

				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				if (indexPath.Section == AnswersSection)
                {
					MakeActionSheet(Answers[indexPath.Row]).ShowInView(RootView);
				}
			}

			private UITableViewCell MakeCell(UITableView tableView, string reuseidentifier)
			{
				return tableView.DequeueReusableCell (reuseidentifier) ?? new UITableViewCell (UITableViewCellStyle.Value2, reuseidentifier);
			}

			private UITableViewCell MakeCellForAnswer(UITableView tableView, string reuseidentifier)
			{
				return tableView.DequeueReusableCell (reuseidentifier) ?? new UITableViewCell (UITableViewCellStyle.Value1, reuseidentifier);
			}

			private UIActionSheet MakeActionSheet(AnswerItemViewModel answer)
			{
				string[] actions = new string[] { "Vote!" };

				UIActionSheet actionSheet = new UIActionSheet("Vote for " + answer.Text + " ?", null, "Cancel", null, actions);
				actionSheet.Clicked += (sender, e) =>
                {
                    if (e.ButtonIndex == 0)
                    {
                        answer.VoteCommand.Execute();
                    }
				};
				return actionSheet;
			}
		}

	}
}

