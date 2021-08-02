using FacebookCloneApp.Common;
using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FacebookCloneApp.ViewModels
{
    class FriendsViewModel : ViewModelBase
    {
        static FaceBookCloneEntities dbContext = new FaceBookCloneEntities();
        //IRepository<User> Repository = new FaceBookRepository<User>();
        UnitOfWork Unit = new UnitOfWork(new FaceBookCloneEntities());
        

        private ObservableCollection<User> _UserList;
        private ObservableCollection<User> _FriendsList;
        private User _selectedUser;
        private ICommand _AcceptRequest;
        private ICommand _UnFriend;

        #region Properties
        public ICommand UnFriend
        {
            get { return _UnFriend; }
            set
            {
                _UnFriend = value;
                OnPropertyChanged("UnFriend");
            }
        }
        public ICommand AcceptRequest
        {
            get { return _AcceptRequest; }
            set
            {
                _AcceptRequest = value;
                OnPropertyChanged("AcceptRequest");
            }
        }
        public ObservableCollection<User> FriendsList
        {
            get { return _FriendsList; }
            set
            {
                _FriendsList = value;
                OnPropertyChanged("FriendsList");
            }
        }
        public ObservableCollection<User> UserList
        {
            get { return _UserList; }
            set
            {
                _UserList = value;
                OnPropertyChanged("UserList");
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
        #endregion

        public FriendsViewModel()
        {
            AcceptRequest = new CommandHandler(new Action<object>(AcceptRequestClick));
            UnFriend = new CommandHandler(new Action<object>(UnFriendClick));

            LoadFriendRequestUsers();
            LoadFriends();
        }

        #region Public Methods
        void LoadFriendRequestUsers()
        {
            try
            {                
                UserList = new ObservableCollection<User>(Unit.UserRepo<User>().LoadFriendRequestUsers());
            }
            catch (Exception ex)
            {
                
            }
        }
        void LoadFriends()
        {
            try
            {
                FriendsList = new ObservableCollection<User>(Unit.UserRepo<User>().LoadFriends());
            }
            catch (Exception ex)
            {
                
            }
        }


        #endregion

        #region Command Method
        private void AcceptRequestClick(object p = null)
        {
            Unit.UserRepo<User>().AcceptFriendRequest(SelectedUser);
            LoadFriendRequestUsers();
        }
        private void UnFriendClick(object p = null)
        {
            Unit.UserRepo<User>().UnFriend(SelectedUser);
            LoadFriends();
        }
        #endregion
    }
}
