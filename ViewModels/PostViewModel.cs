using FacebookCloneApp.Common;
using FacebookCloneApp.Model;
using FacebookCloneApp.Views;
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
    class PostViewModel : ViewModelBase
    {
        static FaceBookCloneEntities dbContext = new FaceBookCloneEntities();
        //IRepository<UserPost> Repository = new FaceBookRepository<UserPost>();
        UnitOfWork Unit = new UnitOfWork(new FaceBookCloneEntities());
        private ObservableCollection<UserPost> _PublicFeeds;
        private UserPost _selectedPost;
        private ICommand _LikePost;
        private ICommand _SearchCommand;
        private ICommand _PostComment;
        private ICommand _ViewComments;
        private string _SearchText;
        private string _CommentText;
        private ObservableCollection<string> _UserComments;

        #region Properties
        public ObservableCollection<string> UserComments
        {
            get { return _UserComments; }
            set
            {
                _UserComments = value;
                OnPropertyChanged("UserComments");
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
        public ICommand LikePost
        {
            get { return _LikePost; }
            set
            {
                _LikePost = value;
                OnPropertyChanged("LikePost");
            }
        }
        public ICommand PostComment
        {
            get { return _PostComment; }
            set
            {
                _PostComment = value;
                OnPropertyChanged("PostComment");
            }
        }
        public ICommand ViewComments
        {
            get { return _ViewComments; }
            set
            {
                _ViewComments = value;
                OnPropertyChanged("ViewComments");
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
        public string CommentText
        {
            get { return _CommentText; }
            set
            {
                _CommentText = value;
                OnPropertyChanged("CommentText");
            }
        }
        public ObservableCollection<UserPost> PublicFeeds
        {
            get { return _PublicFeeds; }
            set
            {
                _PublicFeeds = value;
                OnPropertyChanged("PublicFeeds");
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
        #endregion

        public PostViewModel()
        {
            LikePost = new CommandHandler(new Action<object>(LikePostClick));
            SearchCommand = new CommandHandler(new Action<object>(SearchCommandClick));
            PostComment = new CommandHandler(new Action<object>(PostCommentClick));
            ViewComments = new CommandHandler(new Action<object>(ViewCommentsClick));

            LoadPublicPosts();

            LoadComments();
        }

        #region Public Methods
        void LoadPublicPosts()
        {
            try
            {
                PublicFeeds = new ObservableCollection<UserPost>(Unit.UserPostRepo<UserPost>().LoadPublicPosts());
            }
            catch (Exception ex)
            {

            }
        }

        void LoadComments()
        {
            if (Global.Post != null && Global.Post.Id > 0)
            {
                UserComments = new ObservableCollection<string>();
                var comments = Unit.UserCommentsRepo<UserComment>().LoadComments();
                if (comments != null && comments.Count > 0)
                {
                    foreach (var c in comments)
                    {
                        UserComments.Add(c.CommentText + "\nBy  " + c.User.FullName);
                    }

                }
            }
        }


        #endregion

        #region Command Method
        private void LikePostClick(object p = null)
        {
            Unit.UserPostRepo<UserPost>().LikePost(SelectedPost);
            LoadPublicPosts();
        }
        private void PostCommentClick(object p = null)
        {
            Unit.UserCommentsRepo<UserPost>().PostComment(SelectedPost);
        }
        private void ViewCommentsClick(object p = null)
        {
            if (SelectedPost != null)
            {
                Global.Post = SelectedPost;
                Window mainWin = new ViewComments();
                mainWin.ShowDialog();
            }
        }
        private void SearchCommandClick(object o = null)
        {

            PublicFeeds = new ObservableCollection<UserPost>(Unit.UserPostRepo<UserPost>().SearchPosts(SearchText));
        }
        #endregion
    }
}
