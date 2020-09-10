using System;
using System.Collections.Generic;
using Foundation;
using PasswordValidate.Models;
using UIKit;

namespace PasswordValidate.Views
{
    public class UsersTableSource : UITableViewSource
    {
        private List<User> _users;
        private NSString dequeCellidentifier = new NSString("UserTableViewCell");
        public UsersTableSource(List<User> users)
        {
            _users = users;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(dequeCellidentifier);
            User user = _users[indexPath.Row];

            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, dequeCellidentifier);

            cell.TextLabel.Text = user.UserName;
            cell.DetailTextLabel.Text = $"{user.FirstName} {user.LastName}";

            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _users.Count;
        }
    }
}
