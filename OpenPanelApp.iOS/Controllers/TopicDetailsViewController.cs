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
	public class TopicDetailsViewController : UIViewController
	{
		public Topic Topic { get; set;}
		public UITableView TableView { get; set; }

		public TopicDetailsViewController (Topic topic)
		{
			Topic = topic;
			Title = topic.User.Name;;
		}

		public override void LoadView ()
		{
			base.LoadView ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView = new UITableView (RectangleF.Empty, UITableViewStyle.Grouped) {
				Frame = this.ContentFrame()
			};
			TableView.Source = new TableSource () {
				Topic = this.Topic
			};
//			TableView.Topic = Topic;
			View.AddSubview (TableView);
		}

		private UIButton makeVoteButton ()
		{
			UIButton btnVote = UIButton.FromType(UIButtonType.RoundedRect);
	   		btnVote.Frame = new RectangleF(100f, 100f, 100f, 100f);
   			btnVote.SetTitle("Vote!", UIControlState.Normal);
			btnVote.TouchUpInside += (sender, e) => {
				var a = makeActionSheet();
				a.ShowInView(this.View);
			};

			return btnVote;
		}

		private UIActionSheet makeActionSheet ()
		{
			string[] actions = new string[] { "Yes", "No" };

			UIActionSheet actionsSheet = new UIActionSheet("Your vote?",null,"Cancel",null,actions);
			return actionsSheet;
		}

		class TableSource : UITableViewSource
		{
			private const string CreatorCellId = "CreatorCellId";
			private const string QuestionCellId = "QuestionCellId";
			private const string AnswerCellId = "AnswerCellId";

			public Topic Topic { set; get; }

			private const int NbOfSections = 3;
			private const int CreatorSection = 0;
			private const int QuestionSection = 1;
			private const int AnswersSection = 2;

			public TableSource ()
			{

			}

			public override int NumberOfSections (UITableView tableView)
			{
				return NbOfSections;
			}

			public override string TitleForHeader (UITableView tableView, int section)
			{
				if (section == AnswersSection)
					return "Answers";
				else
					return String.Empty;
			}
			public override int RowsInSection (UITableView tableview, int section)
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
					nbOfRows = Topic.Answers.Count;
					break;
				}

				return nbOfRows;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = null;
				switch (indexPath.Section) {
				case CreatorSection:
					cell = MakeCell(tableView, CreatorCellId);
					switch(indexPath.Row) {
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
					cell = MakeCell(tableView, CreatorCellId);
					cell.TextLabel.Text = "Question";
					cell.DetailTextLabel.Text = Topic.Question;
					break;
				case AnswersSection:
					cell = MakeCellForAnswer(tableView, CreatorCellId);
					cell.TextLabel.Text = Topic.Answers[indexPath.Row].Text;
					cell.DetailTextLabel.Text = Topic.Answers[indexPath.Row].Vote.ToString();
					break;
				}


				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{

			}

			private UITableViewCell MakeCell(UITableView tableView, string reuseidentifier)
			{
				return tableView.DequeueReusableCell (reuseidentifier) ?? new UITableViewCell (UITableViewCellStyle.Value2, reuseidentifier);
			}

			private UITableViewCell MakeCellForAnswer (UITableView tableView, string reuseidentifier)
			{
				return tableView.DequeueReusableCell (reuseidentifier) ?? new UITableViewCell (UITableViewCellStyle.Value1, reuseidentifier);
			}
		}

	}
}

