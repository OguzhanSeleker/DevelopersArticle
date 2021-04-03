using DevelopersArticle.BLL.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class EditArticle : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    var article = DbManager.GetArticleById(id);
                    if (article.Success)
                    {
                        if (article.Data != null)
                        {
                            TxtbxArticleTitle.Text = article.Data.ArticleTitle;
                            TxtAreaArticleContent.Text = article.Data.ArticleContent;
                            imgCurrent.ImageUrl = article.Data.ImageUrl;
                            var dev = DbManager.GetDevelopers().Data.Select(d => new { d.ObjectID, d.FullName });
                            DdlDevelopers.DataSource = dev;
                            DdlDevelopers.SelectedValue = article.Data.WriterId.ToString();
                            DdlDevelopers.DataBind();
                            var cat = DbManager.GetCategories().Data;
                            LbCategories.DataSource = cat.Select(c => new { c.ObjectID, c.CategoryName });
                            LbCategories.DataTextField = "CategoryName";
                            LbCategories.DataValueField = "ObjectID";

                            LbCategories.DataBind();

                            foreach (var category in article.Data.Categories)
                            {
                                LbCategories.Items.FindByValue(category.ObjectID.ToString()).Selected = true;
                            }
                        }
                    }
                    else
                    {
                        InfoLabel(article.Message);
                    }

                }
                else
                {
                    divEdit.Visible = false;
                    InfoLabel(Messages.CheckNullId);
                }
            }
        }

        protected void BtnEditArticle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            byte[] bytes = null;
            if (FlUpldImageUpload.HasFile)
            {
                HttpPostedFile postedFile = FlUpldImageUpload.PostedFile;
                if (postedFile.ContentType.Contains("image") && postedFile.ContentLength < 204800)
                {
                   
                    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                    {
                        bytes = br.ReadBytes(postedFile.ContentLength);
                    }
                }
            }
            if (bytes == null)
            {
                bytes = DbManager.GetArticleById(id).Data.ArticlePictureURL;
            }
            List<int> categoryValues = new List<int>();
            foreach (ListItem item in LbCategories.Items)
            {
                if (item.Selected)
                {
                    categoryValues.Add(Int32.Parse(item.Value));
                }

            }
            int writerId = Int32.Parse(DdlDevelopers.SelectedValue);
            var res = DbManager.UpdateArticle(id, TxtbxArticleTitle.Text, bytes, TxtAreaArticleContent.Text, categoryValues, writerId);
            if (res.Success)
            {
                Application["Info"] = res.Message;
                Response.Redirect("Default.aspx");
            }
            else
            {
                InfoLabel(res.Message);
            }
        }
    }
}