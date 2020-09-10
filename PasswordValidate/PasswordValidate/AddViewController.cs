using System;
using System.Text.RegularExpressions;
using Foundation;
using PasswordValidate.Models;
using UIKit;

namespace PasswordValidate
{
    public partial class AddViewController : UIViewController
    {
        public AddViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //Dismiss the Keyboards for the Text Fields.
            txtFirstName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            txtLastName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            txtUserName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            txtPassword.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void BtnAdd_TouchUpInside(UIButton sender)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text)
                || string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                var emptyFieldAlert = UIAlertController.Create("Required Field Empty", "A required field is not entered", UIAlertControllerStyle.Alert);
                emptyFieldAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                this.PresentViewController(emptyFieldAlert, true, null);
                return;
            }

            //String validations for Password
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            var isBetween5And12Chars = new Regex(@".{5,12}");
            //Length has to be between 5 and 12
            if (!isBetween5And12Chars.IsMatch(txtPassword.Text))
            {
                var passwordLengthAlert = UIAlertController.Create("Password Validation", "Password must be between 5 and 12 characters", UIAlertControllerStyle.Alert);
                passwordLengthAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                this.PresentViewController(passwordLengthAlert, true, null);
                return;
            }

            //Has atleast one number
            if (!hasNumber.IsMatch(txtPassword.Text))
            {
                var passwordLengthAlert = UIAlertController.Create("Password Validation", "Password must have atleast one number", UIAlertControllerStyle.Alert);
                passwordLengthAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                this.PresentViewController(passwordLengthAlert, true, null);
                return;
            }

            //Has atleast one letter
            if (!hasUpperChar.IsMatch(txtPassword.Text) && !hasLowerChar.IsMatch(txtPassword.Text))
            {
                var passwordLengthAlert = UIAlertController.Create("Password Validation", "Password must have atleast one letter", UIAlertControllerStyle.Alert);
                passwordLengthAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                this.PresentViewController(passwordLengthAlert, true, null);
                return;
            }

            //Cannot have special characters
            if (hasSymbols.IsMatch(txtPassword.Text))
            {
                var passwordLengthAlert = UIAlertController.Create("Password Validation", "Password cannot have special characters", UIAlertControllerStyle.Alert);
                passwordLengthAlert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                this.PresentViewController(passwordLengthAlert, true, null);
                return;
            }

            var newUser = new User { FirstName = txtFirstName.Text, LastName = txtLastName.Text, UserName = txtUserName.Text, Password = txtPassword.Text };

            var users = new UsersCollection();
            users.AddUser(newUser).GetAwaiter();

            NSNotificationCenter.DefaultCenter.PostNotificationName("NewUserAdded", this);

            this.DismissViewController(true, null);


        }

        partial void BtnCancel_TouchUpInside(UIButton sender)
        {
            this.DismissViewController(true, null);
        }
    }
}

