using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class AllArticles : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var articles = DbManager.GetAllArticles();
                if (articles.Success)
                {

                    rptArticleList.DataSource = articles.Data.OrderByDescending(a => a.ModifiedDate);
                    rptArticleList.DataBind();
                }
                else
                {
                    InfoLabel(articles.Message);
                }
            }
        }
    }
}