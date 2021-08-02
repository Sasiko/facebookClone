using FacebookCloneApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FacebookCloneApp.Common
{
    static class Global
    {
        static FaceBookCloneEntities dbContext = new FaceBookCloneEntities();
        private static User _loggeduser;

        public static User Loggeduser
        {
            get { return _loggeduser; }
            set { _loggeduser = value; }
        }
        private static UserPost _post;

        public static UserPost Post
        {
            get { return _post; }
            set { _post = value; }
        }
    }
    
}
