using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FacebookCloneApp.Common
{
    public class UserPostRepository<TEntity> : IUserPostRepository<TEntity> where TEntity : class
    {
        private FaceBookCloneEntities dbContext;

        public UserPostRepository(FaceBookCloneEntities _dbContext)
        {
            dbContext = _dbContext;
        }
        public void AddPost(string filePath, string PostText)
        {
            string path = "";
            if (!string.IsNullOrEmpty(filePath))
            {
                string folder = @"Posts\";
                path = Path.Combine(folder, Path.GetFileName(filePath));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (File.Exists(path))
                    File.Delete(path);
                File.Copy(filePath, path);
            }

            UserPost post = new UserPost();
            post.PostText = PostText;
            if (!string.IsNullOrEmpty(path))
                post.Img = Path.GetFullPath(path);
            post.UserId = Global.Loggeduser.Id;
            post.Likes = 0;
            post.CreatedAt = DateTime.Now;

            dbContext.UserPosts.Add(post);
            dbContext.SaveChanges();
            MessageBox.Show("New Post Created..!");
        }

        public void DeletePost(UserPost SelectedPost)
        {
            if (SelectedPost == null)
            {
                MessageBox.Show("Please select a post");
                return;
            }
            var post = dbContext.UserPosts.First(x => x.Id == SelectedPost.Id);
            dbContext.UserPosts.Remove(post);
            dbContext.SaveChanges();
            MessageBox.Show("Post Deleted..!");
        }
        public List<UserPost> LoadMyFeeds()
        {
            return dbContext.UserPosts?.Where(x => x.UserId == Global.Loggeduser.Id)?.ToList();
        }
        public void LikePost(UserPost SelectedPost)
        {
            if (SelectedPost == null)
            {
                MessageBox.Show("Please select a Post first");
                return;
            }
            var post = dbContext.UserPosts.First(x => x.Id == SelectedPost.Id);
            post.Likes++;
            dbContext.SaveChanges();
        }
        public List<UserPost> SearchPosts(string SearchText)
        {
            var PublicFeeds = new List<UserPost>();
            var userId = Global.Loggeduser.Id;
            List<int> friendreq1 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend1ID)?.Distinct()?.ToList();
            var friendreq2 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend2ID)?.Distinct().ToList();
            friendreq1.AddRange(friendreq2);
            friendreq1.Remove(userId);
            var posts = dbContext?.UserPosts?.Where(x => friendreq1.Contains(x.UserId))?.ToList();

            if (string.IsNullOrEmpty(SearchText))
            {
                PublicFeeds = new List<UserPost>(posts);
            }
            else
            {
                PublicFeeds = new List<UserPost>(posts.Where(x => x.PostText.Contains(SearchText))?.ToList());
            }
            return PublicFeeds;
        }
        public List<UserPost> LoadPublicPosts()
        {
            var userId = Global.Loggeduser.Id;
            List<int> friendreq1 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend1ID)?.Distinct()?.ToList();
            var friendreq2 = dbContext.Friends?.Where(p => p.Friend1ID == userId || p.Friend2ID == userId)?.Select(x => x.Friend2ID)?.Distinct().ToList();
            friendreq1.AddRange(friendreq2);
            friendreq1.Remove(userId);
            return dbContext?.UserPosts?.Where(x => friendreq1.Contains(x.UserId))?.ToList();
        }
    }
}
