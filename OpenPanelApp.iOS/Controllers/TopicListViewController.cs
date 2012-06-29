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
	public class TopicListViewController : MvxTouchViewController<TopicListViewModel>
	{
		public UITableView TableView { get; set; }

		public TopicListViewController(MvxShowViewModelRequest request) : base(request)
		{
			Title = "Topics";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Topics")
                {
					((TableSource)TableView.Source).Topics = ViewModel.Topics;
					TableView.ReloadData();
				}
			};

			TableView = new UITableView (RectangleF.Empty, UITableViewStyle.Plain)
            {
                Frame = this.ContentFrame(),
                RowHeight = 60,
                Source = new TableSource(ViewModel.Topics)
            };
			View.AddSubview (TableView);
		}

		class TableSource : UITableViewSource
		{
			private static string CellId = "DefaultCellId";

			public List<TopicItemViewModel> Topics {get; set;} 

			public TableSource(List<TopicItemViewModel> topics)
			{
                Topics = topics;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return Topics.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellId) ?? new UITableViewCell (UITableViewCellStyle.Subtitle, CellId);

				var topic = Topics[indexPath.Row];

				cell.TextLabel.Text = topic.Question;
				cell.DetailTextLabel.Text = topic.User;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
                Topics[indexPath.Row].ViewDetailCommand.Execute();
			}
		}
	}
}

