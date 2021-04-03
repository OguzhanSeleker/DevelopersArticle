using DevelopersArticle.BLL.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class Categories : BasePage
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
                rptKategoriler.DataSource = DbManager.GetCategories().Data;
                rptKategoriler.DataBind();
            }
        }

        protected void AddCategory_OnClick(object sender, EventArgs e)
        {
            var res = DbManager.AddCategory(TxtKategoriAdi.Text);
            if (res.Success)
            {
               
                Application["Info"] = res.Message;
                Response.Redirect("Categories.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
                InfoLabel(res.Message);
        }

        protected void rptKategoriler_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptDevelopersInCategory = (Repeater)e.Item.FindControl("rptDevelopersInCategory");
                Repeater rptArticlesInCategory = (Repeater)e.Item.FindControl("rptArticlesInCategory");
                HiddenField hfCategoryId = (HiddenField)e.Item.FindControl("hfId");
                int categoryid = Int32.Parse(hfCategoryId.Value);
                var developers = DbManager.GetDevelopersFullNameInCategory(categoryid);
                if (developers.Success)
                {
                    rptDevelopersInCategory.DataSource = developers.Data;
                    rptDevelopersInCategory.DataBind();
                }
                var articles = DbManager.GetArticlesInCategory(categoryid);
                if (articles.Success)
                {
                    var test = articles.Data.Select(a => a.ArticleTitle).ToList();
                    if (test.Count == 0)
                    {
                        test.Add("kategoride yazı yok");
                        rptArticlesInCategory.DataSource = test;
                    }
                    else
                    {
                        rptArticlesInCategory.DataSource = articles.Data.Select(a => a.ArticleTitle).ToList();
                    }
                    rptArticlesInCategory.DataBind();
                }
            }
        }

        protected void btnKategoriDuzenle_Click(object sender, EventArgs e)
        {
            var res = DbManager.UpdateCategoryName(int.Parse(hfCategoryId.Value), txtEditModal.Text);
            if (res.Success)
            {
                Application["Info"] = res.Message;
                Response.Redirect("Categories.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                InfoLabel(res.Message);
            }
        }

        private void KategoriSil(int categoryId)
        {
            var deleteCategory = DbManager.GetCategoryById(categoryId);
            if (deleteCategory.Data.Developers.Count != 0 || deleteCategory.Data.Articles.Count != 0)
            {
                InfoLabel(Messages.ErrorDeleteCategory);

            }
            else
            {
                var res = DbManager.DeleteCategory(categoryId);
                if (res.Success)
                {
                    Application["Info"] = res.Message;
                    Response.Redirect("Categories.aspx");
                    Context.ApplicationInstance.CompleteRequest();
                    
                }
                else
                {
                    InfoLabel(res.Message);
                }
            }

        }
    

    protected void rptKategoriler_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int categoryId = int.Parse((string)(e.CommandArgument));
        if (e.CommandName != "Delete")
        {
            txtEditModal.Text = DbManager.GetCategoryById(categoryId).Data.CategoryName;
            hfCategoryId.Value = categoryId.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        else
        {
            KategoriSil(categoryId);
        }
    }
}
}