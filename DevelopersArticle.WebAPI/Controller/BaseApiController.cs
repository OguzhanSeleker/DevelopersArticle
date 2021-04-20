using DevelopersArticle.BLL;
using DevelopersArticle.BLL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevelopersArticle.WebAPI.Controller
{
    public class BaseApiController : ApiController
    {
        IService dbManager;
        public IService DbManager
        {
            get
            {
                if (dbManager == null)
                {
                    dbManager = new DBManager();
                }
                return dbManager;
            }
        }

    }
}