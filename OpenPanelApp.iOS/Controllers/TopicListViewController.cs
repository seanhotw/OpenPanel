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

		public TopicListViewController (MvxShowViewModelRequest request) : base(request)
		{
			Title = "Topics";
		}

		public override void LoadView ()
		{
			base.LoadView ();

			TableView = new UITableView (RectangleF.Empty, UITableViewStyle.Plain)
            {
                Frame = this.ContentFrame(),
                RowHeight = 60
            };
			TableView.Source = new TableSource () {
				ViewModel = this.ViewModel
			};
			View.AddSubview (TableView);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ViewModel.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == "Topics") {
					((TableSource)TableView.Source).ViewModel = ViewModel;
					TableView.ReloadData ();
				}
			};
			ViewModel.Search ();
		}

		class TableSource : UITableViewSource
		{
			private static string CellId = "DefaultCellId";

			public TopicListViewModel ViewModel {get; set;} 

			public TableSource ()
			{
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				ViewModel.ToString();
				return ViewModel.Topics.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell (CellId) ?? new UITableViewCell (UITableViewCellStyle.Subtitle, CellId);

				var topic = ViewModel.Topics [indexPath.Row];

				cell.TextLabel.Text = topic.Question;
				cell.DetailTextLabel.Text = topic.User.Name;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				ViewModel.SelectTopic(ViewModel.Topics[indexPath.Row]);
			}
		}
	}
}

