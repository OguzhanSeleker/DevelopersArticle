using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["Info"] != null)
                {
                    InfoLabel(Application["Info"].ToString());
                }
                var articles = DbManager.GetAllArticles();
                if (articles.Success)
                {
                    rptArticleList.DataSource = articles.Data;
                    rptArticleList.DataBind();
                }
                 
                
            }
        }
    }

}