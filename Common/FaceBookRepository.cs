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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FacebookCloneApp.Common
{
    public class FaceBookRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private FaceBookCloneEntities dbContext;

        public FaceBookRepository(FaceBookCloneEntities _dbContext)
        {
            dbContext = _dbContext;
        }
        

    }
}
