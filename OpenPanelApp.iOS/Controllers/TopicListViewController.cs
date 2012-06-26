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
        }

        public override void LoadView()
        {
            base.LoadView();

            TableView = new UITableView(RectangleF.Empty, UITableViewStyle.Plain)
            {
                Frame = this.ContentFrame(),
                Source = new TopicsTableSource(),
                RowHeight = 60
            };
            View.AddSubview(TableView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Topics")
                {
                    ((TopicsTableSource)TableView.Source).Topics = ViewModel.Topics;
                    TableView.ReloadData();
                }
            };
            ViewModel.Search();
        }
    }

    public class TopicsTableSource : UITableViewSource
    {
        private static string CellId = "DefaultCellId";
        public List<Topic> Topics { get; set; }

        public TopicsTableSource()
        {
            Topics = new List<Topic>();
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return Topics.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellId) ?? new UITableViewCell(UITableViewCellStyle.Subtitle, CellId);

            var topic = Topics[indexPath.Row];

            cell.TextLabel.Text = topic.Title;
            cell.DetailTextLabel.Text = topic.CreatedBy.Name;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
        }
    }
}

