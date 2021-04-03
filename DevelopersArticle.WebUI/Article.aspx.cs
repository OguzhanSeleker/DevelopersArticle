using DevelopersArticle.BLL.Utilities.Constants;
using System;
using System.Linq;
using System.Web.UI;

namespace DevelopersArticle.WebUI
{
    public partial class Article : BasePage
    {
        private int commentId;
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
                        rptArtCats.DataSource = article.Data.Categories.Select(c => c.CategoryName);
                        rptArtCats.DataBind();
                        rptComments.DataSource = article.Data.Comments.Where(c => c.IsDeleted == false).OrderBy(c => c.ModifiedDate).ToList();
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
                else if (Request.QueryString["Category"] != null)
                {
                    var article = DbManager.GetArticlesByCategoryId(int.Parse(Request.QueryString["Category"]));
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


        protected void rptComments_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            commentId = int.Parse((string)(e.CommandArgument));

            if (e.CommandName == "Edit")
            {
                var res = DbManager.GetCommentById(commentId);
                if (res.Success)
                {
                    tbEditContent.Text = res.Data.CommentContent;
                    hfCommentId.Value = res.Data.ObjectID.ToString();
                    var dev = DbManager.GetDevelopers();
                    if (dev.Success)
                    {
                        ddlModalWriter.DataSource = dev.Data.Select(c => new { c.FullName, c.ObjectID });
                        ddlModalWriter.DataBind();
                        ddlModalWriter.SelectedValue = res.Data.Developer.ObjectID.ToString();
                       
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditCommentModal();", true);
                    }
                }
            }
            else
            {
                var res = DbManager.DeleteComment(commentId);
                if (res.Success)
                {
                    Response.Redirect("Article.aspx?id=" + Request.QueryString["id"]);
                }
                else
                {
                    InfoLabel(res.Message);
                }
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int commentId = int.Parse(hfCommentId.Value);
            var res = DbManager.UpdateComment(commentId, tbEditContent.Text, int.Parse(ddlModalWriter.SelectedValue));
            if (res.Success)
            {
                Application["Info"] = res.Message;
                Response.Redirect("Article.aspx?id=" + hfArticleId.Value);
            }
            else
            {
                InfoLabel(res.Message);
            }
        }
    }
}