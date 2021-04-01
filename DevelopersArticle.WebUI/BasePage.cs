using DevelopersArticle.BLL.Concrete;
using DevelopersArticle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public class BasePage : Page
    {
        private DBManager dbManager;
        public DBManager DbManager
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


        public void InfoLabel(string infoText)
        {
            Label LblInfo = (Label)Page.Master.FindControl("LblInfo");
            LblInfo.Visible = true;
            LblInfo.Text = infoText;
        }

    }
}