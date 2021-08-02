using FacebookCloneApp.Common;
using FacebookCloneApp.Model;
using FacebookCloneApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FacebookCloneApp.ViewModels
{
    class UserViewModel : ViewModelBase
    {
        //        IRepository<User> Repository = new FaceBookRepository<User>();
        UnitOfWork Unit = new UnitOfWork(new FaceBookCloneEntities());
        private ICommand _LoginCommand;
        private ICommand _OpenRegisterClick;
        private ICommand _OpenLoginClick;
        private ICommand _AddNewUserClick;
        private string _username;
        private string _password;
        private string _FullName;
        private ComboBoxItem _Gender;
        private string _Contact;
        private string _Email;

        #region Constructor
        public UserViewModel()
        {

            LoginCommand = new CommandHandler(new Action<object>(Login));
            OpenRegisterClick = new CommandHandler(new Action<object>(OpenRegisterWindow));
            OpenLoginClick = new CommandHandler(new Action<object>(OpenLoginWindow));
            AddNewUserClick = new CommandHandler(new Action<object>(AddNewUser));
        }
        #endregion


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public string Contact
        {
            get { return _Contact; }
            set
            {
                _Contact = value;
                OnPropertyChanged("Contact");
            }
        }
        public string FullName
        {
            get { return _FullName; }
            set
            {
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }
        public ComboBoxItem Gender
        {
            get { return _Gender; }
            set
            {
                _Gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public ICommand LoginCommand
        {
            get { return _LoginCommand; }
            set
            {
                _LoginCommand = value;
                OnPropertyChanged("LoginCommand");
            }
        }
        public ICommand OpenRegisterClick
        {
            get { return _OpenRegisterClick; }
            set
            {
                _OpenRegisterClick = value;
                OnPropertyChanged("OpenRegisterClick");
            }
        }
        public ICommand OpenLoginClick
        {
            get { return _OpenLoginClick; }
            set
            {
                _OpenLoginClick = value;
                OnPropertyChanged("OpenLoginClick");
            }
        }
        public ICommand AddNewUserClick
        {
            get { return _AddNewUserClick; }
            set
            {
                _AddNewUserClick = value;
                OnPropertyChanged("AddNewUserClick");
            }
        }


        #region Command Method
        private void AddNewUser(object p = null)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || Gender == null ||
                string.IsNullOrEmpty(Contact) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(FullName))
            {
                MessageBox.Show("Please provide all details");
                return;
            }
            else
            {
                var _loggedUser = Unit.UserRepo<User>().AddNewUser(Contact, FullName, Email, Gender.Content.ToString(), Username, Password);
                if (_loggedUser != null)
                {
                    MessageBox.Show("Registered Successfullly!");
                    Global.Loggeduser = _loggedUser;
                    foreach (Window item in Application.Current.Windows)
                    {
                        if (item.DataContext == this)
                        {
                            Window mainWin = new MainWindow();
                            mainWin.Show();
                            item.Close();
                        }
                    }

                }
                else
                {
                }

            }

        }
        private void Login(object p = null)
        {
            var _loggedUser = Unit.UserRepo<User>().GetUser(Username, Password);

            if (_loggedUser != null)
            {
                Global.Loggeduser = _loggedUser;
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this)
                    {
                        Window mainWin = new MainWindow();
                        mainWin.Show();
                        item.Close();
                    }
                }

            }
            else
            {
            }

        }
        private void OpenRegisterWindow(object p = null)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this)
                {
                    Window mainWin = new Register();
                    mainWin.Show();

                    item.Close();
                }
            }
        }
        private void OpenLoginWindow(object p = null)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this)
                {
                    Window mainWin = new Login();
                    mainWin.Show();

                    item.Close();
                }
            }
        }
        #endregion
    }
}
