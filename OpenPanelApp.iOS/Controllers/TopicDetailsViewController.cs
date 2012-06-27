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

		public TopicDetailsViewController (Topic topic)
		{
			Topic = topic;
			Title = topic.CreatedBy.Name;;
		}

		public override void LoadView ()
		{
			base.LoadView ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.AddSubview(makeVoteButton());
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
			private static string CellId = "DetailsCellId";

			private TopicDetailsViewController controller;

			private const int NbOfSections = 3;
			private const int CreatorSection = 0;
			private const int QuestionSection = 1;
			private const int OpinionsSection = 2;

			public TableSource (TopicDetailsViewController controller)
			{
				this.controller = controller;
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
				case OpinionsSection:
					// nb of answers
					nbOfRows = controller.Topic.Answers.Count;
					break;
				}

				return nbOfRows;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return NbOfSections;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellId) ?? new UITableViewCell (UITableViewCellStyle.Subtitle, CellId);
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				    
			}
		}

	}
}

