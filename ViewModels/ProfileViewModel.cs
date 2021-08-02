using FacebookCloneApp.Common;
using FacebookCloneApp.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FacebookCloneApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        static FaceBookCloneEntities dbContext = new FaceBookCloneEntities();
        //IRepository<UserPost> Repository = new FaceBookRepository<UserPost>();
        UnitOfWork Unit = new UnitOfWork(new FaceBookCloneEntities());
        private ObservableCollection<User> _UserList;
        private ObservableCollection<UserPost> _MyFeeds;
        private User _selectedUser;
        private UserPost _selectedPost;
        private ICommand _ChangeCover;
        private ICommand _ChangeDp;
        private ICommand _ChangeTagline;
        private ICommand _SendRequest;
        private ICommand _AddImage;
        private ICommand _AddPost;
        private ICommand _DeletePost;
        private ICommand _SearchCommand;
        private string _SearchText;
        private ImageSource _CoverSource;
        private ImageSource _DpSource;
        private ImageSource _PostSource;
        private string _Username;
        private string _Tagline;
        private string _PostText;
        private string filePath = "";

        #region Properties
        public ICommand ChangeCover
        {
            get { return _ChangeCover; }
            set
            {
                _ChangeCover = value;
                OnPropertyChanged("ChangeCover");
            }
        }
        public ICommand ChangeDp
        {
            get { return _ChangeDp; }
            set
            {
                _ChangeDp = value;
                OnPropertyChanged("ChangeDp");
            }
        }
        public ICommand SendRequest
        {
            get { return _SendRequest; }
            set
            {
                _SendRequest = value;
                OnPropertyChanged("SendRequest");
            }
        }
        public ICommand ChangeTagline
        {
            get { return _ChangeTagline; }
            set
            {
                _ChangeTagline = value;
                OnPropertyChanged("ChangeTagline");
            }
        }
        public ICommand AddImage
        {
            get { return _AddImage; }
            set
            {
                _AddImage = value;
                OnPropertyChanged("AddImage");
            }
        }
        public ICommand AddPost
        {
            get { return _AddPost; }
            set
            {
                _AddPost = value;
                OnPropertyChanged("AddPost");
            }
        }
        public ICommand DeletePost
        {
            get { return _DeletePost; }
            set
            {
                _DeletePost = value;
                OnPropertyChanged("DeletePost");
            }
        }
        public ICommand SearchCommand
        {
            get { return _SearchCommand; }
            set
            {
                _SearchCommand = value;
                OnPropertyChanged("SearchCommand");
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
        public ObservableCollection<UserPost> MyFeeds
        {
            get { return _MyFeeds; }
            set
            {
                _MyFeeds = value;
                OnPropertyChanged("MyFeeds");
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
        public UserPost SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                _selectedPost = value;
                OnPropertyChanged("SelectedPost");
            }
        }
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged("Username");
            }
        }
        public string PostText
        {
            get { return _PostText; }
            set
            {
                _PostText = value;
                OnPropertyChanged("PostText");
            }
        }
        public string Tagline
        {
            get { return _Tagline; }
            set
            {
                _Tagline = value;
                OnPropertyChanged("Tagline");
            }
        }
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        public ImageSource CoverSource
        {
            get { return _CoverSource; }
            set
            {
                _CoverSource = value;
                OnPropertyChanged("CoverSource");
            }
        }
        public ImageSource DpSource
        {
            get { return _DpSource; }
            set
            {
                _DpSource = value;
                OnPropertyChanged("DpSource");
            }
        }
        public ImageSource PostSource
        {
            get { return _PostSource; }
            set
            {
                _PostSource = value;
                OnPropertyChanged("PostSource");
            }
        }
        #endregion
        public ProfileViewModel()
        {
            ChangeCover = new CommandHandler(new Action<object>(ChangeCoverClick));
            ChangeDp = new CommandHandler(new Action<object>(ChangeDpClick));
            ChangeTagline = new CommandHandler(new Action<object>(ChangeTaglineClick));
            SendRequest = new CommandHandler(new Action<object>(SendRequestClick));
            AddImage = new CommandHandler(new Action<object>(AddImageClick));
            AddPost = new CommandHandler(new Action<object>(AddPostClick));
            DeletePost = new CommandHandler(new Action<object>(DeletePostClick));
            SearchCommand = new CommandHandler(new Action<object>(SearchCommandClick));

            UserList = new ObservableCollection<User>();

            LoadProfileInfo();
            LoadMyFeeds();
        }

        #region Command Method
        private void ChangeCoverClick(object p = null)
        {
            CoverSource = Unit.UserRepo<User>().ChangeCover();
        }

        private void ChangeDpClick(object p = null)
        {
            DpSource = Unit.UserRepo<User>().ChangeDp();

        }
        private void AddImageClick(object p = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|all files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                PostSource = new BitmapImage(fileUri);
                filePath = openFileDialog.FileName;
            }
        }
        private void AddPostClick(object p = null)
        {
            Unit.UserPostRepo<UserPost>().AddPost(filePath, PostText);
        }
        private void DeletePostClick(object p = null)
        {
            Unit.UserPostRepo<UserPost>().DeletePost(SelectedPost);
            LoadMyFeeds();
        }
        private void ChangeTaglineClick(object p = null)
        {
            Unit.UserRepo<User>().ChangeTagline(Tagline);
        }
        private void SendRequestClick(object p = null)
        {
            Unit.UserRepo<User>().SendRequest(SelectedUser);
            LoadUsers();
        }
        private void SearchCommandClick(object o = null)
        {
            UserList = new ObservableCollection<User>();
            UserList = new ObservableCollection<User>(Unit.UserRepo<User>().SearchUsers(SearchText));
        }
        #endregion

        #region Public Methods
        public void LoadProfileInfo()
        {
            Username = Global.Loggeduser.FullName;
            Tagline = dbContext.Users.First(x => x.Id == Global.Loggeduser.Id).Tagline;

            string folder = @"Dp\";
            string fullPath = Path.GetFullPath(folder);
            string path = Path.Combine(fullPath, Global.Loggeduser.Dp);
            Uri fileUri = new Uri(path);
            DpSource = new BitmapImage(fileUri);

            folder = @"Cover\";
            fullPath = Path.GetFullPath(folder);
            path = Path.Combine(fullPath, Global.Loggeduser.Cover);
            fileUri = new Uri(path);
            CoverSource = new BitmapImage(fileUri);

            LoadUsers();
        }
        void LoadUsers()
        {
            UserList = new ObservableCollection<User>(Unit.UserRepo<User>().LoadUsers());
        }


        void LoadMyFeeds()
        {
            MyFeeds = new ObservableCollection<UserPost>();
            var feeds = Unit.UserPostRepo<UserPost>().LoadMyFeeds();
            foreach (var item in feeds)
            {
                MyFeeds.Add(item);
            }

        }
        #endregion
    }
}
