using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookCloneApp.Common
{
    interface IUserCommentsRepository<TEntity>
    {
        List<UserComment> LoadComments();
        void PostComment(UserPost SelectedPost);
    }
}
