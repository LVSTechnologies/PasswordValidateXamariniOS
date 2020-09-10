using Foundation;
using PasswordValidate.Models;
using PasswordValidate.Views;
using System;
using System.Collections.Generic;
using UIKit;

namespace PasswordValidate
{
    public partial class ViewController : UIViewController
    {
        UsersCollection UsersCollection;
        NSObject _listener;
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            UsersCollection = new UsersCollection();
            tableViewUsers.Source = new UsersTableSource(UsersCollection.Users);
        }

        public override void ViewWillAppear(bool animated)
        {
            _listener = NSNotificationCenter.DefaultCenter.AddObserver(new NSString("NewUserAdded"), notification => RefreshTableView());
            base.ViewDidAppear(animated);

        }

        private void RefreshTableView()
        {
            InvokeOnMainThread(delegate {
                UsersCollection = new UsersCollection();
                tableViewUsers.Source = new UsersTableSource(UsersCollection.Users);
                tableViewUsers.ReloadData();
            });
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}