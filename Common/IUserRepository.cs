using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FacebookCloneApp.Common
{
    interface IUserRepository<TEntity>
    {
        void AcceptFriendRequest(User SelectedUser);
        void UnFriend(User SelectedUser);
        List<User> LoadFriendRequestUsers();
        List<User> LoadFriends();

        User AddNewUser(string Contact, string FullName, string Email, string Gender, string Username, string Password);
        User GetUser(string userName, string Password);

        void ChangeTagline(string Tagline);
        void SendRequest(User SelectedUser);
        List<User> SearchUsers(string SearchText);
        List<User> LoadUsers();
        ImageSource ChangeCover();
        ImageSource ChangeDp();
    }
}
