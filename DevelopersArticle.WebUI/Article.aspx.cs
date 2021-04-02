using DevelopersArticle.BLL.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class Article : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    var article = DbManager.GetArticleById(int.Parse(Request.QueryString["id"]));
                    if (article.Success)
                    {
                        imgArticleImage.ImageUrl = article.Data.ImageUrl;
                        lblTitle.Text = article.Data.ArticleTitle;
                        hfArticleId.Value = article.Data.ObjectID.ToString();
                        lblContent.Text = article.Data.ArticleContent;
                        lblWriterFullName.Text = article.Data.Developer.FullName;
                        lblModifiedDate.Text = article.Data.ModifiedDate.ToString();

                        rptComments.DataSource = article.Data.Comments.OrderBy(c => c.ModifiedDate);
                        rptComments.DataBind();

                        var dev = DbManager.GetDevelopers();
                        if (dev.Success)
                        {

                            ddlCommentWriter.DataSource = dev.Data.Select(c => new { c.FullName, c.ObjectID });
                            ddlCommentWriter.DataBind();
                        }
                        hlEditArticle.NavigateUrl = "EditArticle.aspx?id=" + hfArticleId.Value;
                    }
                    else
                    {
                        divArticle.Visible = false;
                        InfoLabel(Messages.CheckNullId);
                    }
                }
                else
                {
                    divArticle.Visible = false;
                    InfoLabel(Messages.CheckNullId);
                  
                }
            }
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            var res = DbManager.AddComment(int.Parse(hfArticleId.Value), int.Parse(ddlCommentWriter.SelectedValue), txtbxCommentContent.Text);
            if (res.Success)
            {
                Response.Redirect("Article.aspx?id=" + hfArticleId.Value);
            }
            else
            {
                InfoLabel(res.Message);
            }
        }

        protected void lbDelArticle_Click(object sender, EventArgs e)
        {
            var deleteArticle = DbManager.DeleteArticle(int.Parse(hfArticleId.Value));
            if (deleteArticle.Success)
            {
                Application["Info"] = deleteArticle.Message;
                Response.Redirect("default.aspx");
            }
            else
            {
                InfoLabel(deleteArticle.Message);
            }
        }
    }
}