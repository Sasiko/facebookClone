using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FacebookCloneApp.Common
{
    public class UserCommentsRepository<TEntity> : IUserCommentsRepository<TEntity> where TEntity : class
    {
        private FaceBookCloneEntities dbContext;

        public UserCommentsRepository(FaceBookCloneEntities _dbContext)
        {
            dbContext = _dbContext;
        }
        public List<UserComment> LoadComments()
        {
            return dbContext.UserComments?.Where(x => x.PostId == Global.Post.Id)?.ToList();
        }
        public void PostComment(UserPost SelectedPost)
        {
            if (SelectedPost != null)
            {
                UserComment comment = new UserComment();
                comment.AddedAt = DateTime.Now;
                comment.CommentText = SelectedPost.Comment;
                comment.PostId = SelectedPost.Id;
                comment.UserId = Global.Loggeduser.Id;

                dbContext.UserComments.Add(comment);
                dbContext.SaveChanges();

                MessageBox.Show("Commant Added");
            }
        }

    }
}
