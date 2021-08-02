using FacebookCloneApp.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FacebookCloneApp.Common
{
    public class UserRepository<TEntity> : IUserRepository<TEntity> where TEntity : class
    {
        private FaceBookCloneEntities dbContext;

        public UserRepository(FaceBookCloneEntities _dbContext)
        {
            dbContext = _dbContext;
        }
        public void AcceptFriendRequest(User SelectedUser)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            var user = dbContext.FriendRequests.First(x => x.P2Id == Global.Loggeduser.Id && x.P1Id == SelectedUser.Id);

            Friend friend = new Friend();
            friend.Friend1ID = Global.Loggeduser.Id;
            friend.Friend2ID = SelectedUser.Id;
            friend.AddedAt = DateTime.Now;
            dbContext.Friends.Add(friend);


            MessageBox.Show("Request Accepted, Now you are friend with " + user.User.FullName);
            dbContext.FriendRequests.Remove(user);
            dbContext.SaveChanges();
        }
        public User AddNewUser(string Contact, string FullName, string Email, string Gender, string Username, string Password)
        {
            var _loggedUser = new User();
            using (var dbContext = new FaceBookCloneEntities())
            {
                _loggedUser.Contact = Contact;
                _loggedUser.FullName = FullName;
                _loggedUser.Email = Email;
                _loggedUser.Gender = Gender;
                _loggedUser.Password = Password;
                _loggedUser.Username = Username;
                _loggedUser.Dp = "profilePic.png";
                _loggedUser.Cover = "profilePic.png";
                _loggedUser.Tagline = "Profile Tagline";
                dbContext.Users.Add(_loggedUser);
                dbContext.SaveChanges();
            }
            return _loggedUser;
        }

        public ImageSource ChangeCover()
        {
            ImageSource CoverSource = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|all files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                CoverSource = new BitmapImage(fileUri);

                string folder = @"Cover\";
                var path = Path.Combine(folder, Path.GetFileName(openFileDialog.FileName));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(path))
                    File.Delete(path);
                File.Copy(openFileDialog.FileName, path);

                var user = dbContext.Users.First(x => x.Id == Global.Loggeduser.Id);
                user.Cover = path;
                dbContext.SaveChanges();
                MessageBox.Show("Profile Updated..!");
            }
            return CoverSource;
        }

        public ImageSource ChangeDp()
        {
            ImageSource DpSource = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|all files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                DpSource = new BitmapImage(fileUri);
                string folder = @"Dp\";
                var path = Path.Combine(folder, Path.GetFileName(openFileDialog.FileName));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(path))
                    File.Delete(path);
                File.Copy(openFileDialog.FileName, path);

                var user = dbContext.Users.First(x => x.Id == Global.Loggeduser.Id);
                user.Dp = path;
                dbContext.SaveChanges();
                MessageBox.Show("Profile Updated..!");
            }
            return DpSource;
        }

        public List<User> LoadFriendRequestUsers()
        {
            var userId = Global.Loggeduser.Id;
            var friendreq = dbContext.FriendRequests?.Where(p => p.P2Id == userId)?.Select(x => x.P1Id)?.Distinct().ToList();
            var users = dbContext?.Users?.Where(x => friendreq.Contains(x.Id))?.ToList();
            return users;
        }

        public List<User> LoadFriends()
        {
            var userId = Global.Loggeduser.Id;
            var friendreq1 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend1ID)?.Distinct().ToList();
            var friendreq2 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend2ID)?.Distinct().ToList();
            friendreq1.AddRange(friendreq2);
            friendreq1.Remove(userId);
            return dbContext?.Users?.Where(x => friendreq1.Contains(x.Id))?.ToList();
        }
        public User GetUser(string userName, string Password)
        {
            var user = dbContext.Users.FirstOrDefault(X => X.Username == userName && X.Password == Password);
            return user;
        }
        public void ChangeTagline(string Tagline)
        {
            var user = dbContext.Users.First(x => x.Id == Global.Loggeduser.Id);
            user.Tagline = Tagline;
            dbContext.SaveChanges();
            MessageBox.Show("Tagline Updated..!");
        }
        public List<User> SearchUsers(string SearchText)
        {
            var Users = dbContext.Users.Where(x => x.Id != Global.Loggeduser.Id).ToList();
            var listOfUsers = new List<User>();
            var UserList = new List<User>();
            listOfUsers.AddRange(Users.ToArray());
            foreach (var item in listOfUsers)
            {
                if (!areFriends(Global.Loggeduser.Id, item.Id))
                {
                    UserList.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(SearchText))
            {
                UserList = UserList.Where(x => x.FullName != null && x.FullName.Contains(SearchText))?.ToList();
            }

            return UserList;
        }

        public void SendRequest(User SelectedUser)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            FriendRequest friend = new FriendRequest();
            friend.P1Id = Global.Loggeduser.Id;
            friend.P2Id = SelectedUser.Id;
            friend.RequestedAt = DateTime.Now;
            dbContext.FriendRequests.Add(friend);
            dbContext.SaveChanges();
            MessageBox.Show("Request Sent..!");
        }

        public void UnFriend(User SelectedUser)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a row first");
                return;
            }
            var user = dbContext.Friends.First(f => (f.Friend1ID == SelectedUser.Id && f.Friend2ID == Global.Loggeduser.Id) || (f.Friend2ID == SelectedUser.Id && f.Friend1ID == Global.Loggeduser.Id));

            MessageBox.Show("Request Accepted, Now you no longer friend with " + user.User.FullName);
            dbContext.Friends.Remove(user);
            dbContext.SaveChanges();
        }
        public List<User> LoadUsers()
        {
            var UserList = new List<User>();
            var Users = dbContext.Users.Where(x => x.Id != Global.Loggeduser.Id).ToList();
            var listOfUsers = new List<User>();
            listOfUsers.AddRange(Users.ToArray());
            foreach (var item in listOfUsers)
            {
                if (!areFriends(Global.Loggeduser.Id, item.Id))
                {
                    UserList.Add(item);
                }
            }

            int n = UserList.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                User user = UserList[k];
                UserList[k] = UserList[n];
                UserList[n] = user;
            }
            return UserList;
        }
        bool areFriends(int ActiveUserId, int userId)
        {
            if (ActiveUserId == userId)
            {
                return false;
            }
            Friend friends = null;

            try
            {
                friends = dbContext.Friends.First(f => (f.Friend1ID == ActiveUserId && f.Friend2ID == userId) || (f.Friend2ID == ActiveUserId && f.Friend1ID == userId));
            }
            catch (Exception ex)
            {
                friends = null;
            }

            if (friends == null)
                return false;

            return true;
        }
    }
}
