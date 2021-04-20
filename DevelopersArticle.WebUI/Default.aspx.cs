using DevelopersArticle.BLL.Utilities.Constants;
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
                    Application.Clear();
                }

                if (Request.QueryString["Category"] != null)
                {
                    var catId = int.Parse(Request.QueryString["Category"]);
                    var article = DbManager.GetArticlesByCategoryId(catId);
                    if (article.Success)
                    {
                        if (article.Data.Count == 0)
                        {
                            InfoLabel(Messages.EmptyCatArt);
                        }
                        else
                        {
                            lblLittleTitle.Text = DbManager.GetCategoryById(catId).Data.CategoryName + " Kategorisine Ait Yazılar";
                            rptArticleList.DataSource = article.Data.OrderByDescending(c => c.ModifiedDate);
                            rptArticleList.DataBind();
                        }
                    }
                }
                else
                {
                    var articles = DbManager.GetAllArticles();
                    if (articles.Success)
                    {
                        lblLittleTitle.Text = "Son Bir Haftada Oluşturulan Yazılar";
                        var datas = articles.Data.Where(c => c.ModifiedDate > DateTime.Now.AddDays(-7)).ToList().OrderByDescending(a => a.ModifiedDate).ToList();
                        rptArticleList.DataSource = articles.Data.Where(c => c.ModifiedDate > DateTime.Now.AddDays(-7)).ToList().OrderByDescending(a => a.ModifiedDate);
                        rptArticleList.DataBind();
                    }
                }



            }
        }
    }

}