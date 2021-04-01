using DevelopersArticle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.BLL.Concrete
{
    public class BaseSingleton
    {
        private DevelopersArticlesEntities _dbInstance;

        public DevelopersArticlesEntities DbInstance
        {
            get
            {
                if (_dbInstance == null)
                {
                    _dbInstance = new DevelopersArticlesEntities();
                }
                return _dbInstance;
            }
        }

    }
}
