using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevelopersArticle.BLL.Utilities.Constants;

namespace DevelopersArticle.WebUI
{
    public partial class AddArticle : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var categories = DbManager.GetCategories();
                if (categories.Success)
                {
                    LbCategories.DataSource = categories.Data.Select(c => new { c.ObjectID, c.CategoryName });
                    LbCategories.DataTextField = "CategoryName";
                    LbCategories.DataValueField = "ObjectID";
                    LbCategories.DataBind();
                }
                else
                {
                    InfoLabel(categories.Message);
                }


                var developers = DbManager.GetDevelopers();
                if (developers.Success)
                {
                    DdlDevelopers.DataSource = developers.Data.Select(d => new { d.ObjectID, d.FullName });
                    DdlDevelopers.DataBind();
                    
                }
                else
                {
                    InfoLabel(developers.Message);
                }

            }

        }

        protected void BtnAddArticle_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlUpldImageUpload.HasFile)
                {
                    HttpPostedFile postedFile = FlUpldImageUpload.PostedFile;
                    if (postedFile.ContentType.Contains("image") && postedFile.ContentLength < 204800)
                    {
                        byte[] bytes;
                        using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                        {
                            bytes = br.ReadBytes(postedFile.ContentLength);
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
                        var result = DbManager.AddArticle(TxtbxArticleTitle.Text, bytes, TxtAreaArticleContent.Text, categoryValues, writerId);
                        if (result.Success == true)
                        {
                            Application["Info"] = result.Message;
                            Response.Redirect("default.aspx");
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            InfoLabel(result.Message);
                        }
                    }
                    else
                    {
                        InfoLabel(Messages.UploadImageError);
                    }
                }
                else
                {
                    InfoLabel(Messages.Error);
                }
            }
            catch (Exception)
            {
                InfoLabel(Messages.Error);
            }

        }
    }
}