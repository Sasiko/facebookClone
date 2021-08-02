using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookCloneApp.Common
{
    interface IUserPostRepository<TEntity>
    {
        void AddPost(string filePath, string PostText);
        void DeletePost(UserPost SelectedPost);
        List<UserPost> LoadMyFeeds();
        void LikePost(UserPost SelectedPost);
        
        List<UserPost> LoadPublicPosts();
        List<UserPost> SearchPosts(string SearchText);
    }
}
