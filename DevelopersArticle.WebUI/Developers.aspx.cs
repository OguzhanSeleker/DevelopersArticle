using DevelopersArticle.BLL.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopersArticle.WebUI
{
    public partial class Developers : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["Info"] != null)
                {
                    InfoLabel(Application["Info"].ToString());
                }
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
                    rptDevelopers.DataSource = developers.Data.Select(d => new { d.FullName, d.UserName, d.ObjectID });
                    rptDevelopers.DataBind();
                }
            }
        }

        protected void AddDeveloper_Click(object sender, EventArgs e)
        {
            List<int> categoryIds = new List<int>();
            foreach (ListItem item in LbCategories.Items)
            {
                if (item.Selected)
                {
                    categoryIds.Add(int.Parse(item.Value));
                }

            }
            var addedDeveloper = DbManager.AddDeveloper(TxtYazarAdi.Text, TxtYazarSoyadi.Text, TxtYazarUsername.Text, categoryIds);
            if (addedDeveloper.Success)
            {
                Application["Info"] = addedDeveloper.Message;
                Response.Redirect("Developers.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                InfoLabel(addedDeveloper.Message);
            }
        }


        protected void rptDevelopers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptDeveloperCategories = (Repeater)e.Item.FindControl("rptDeveloperCategories");
                Repeater rptDeveloperArticles = (Repeater)e.Item.FindControl("rptDeveloperArticles");
                HiddenField hfDeveloperId = (HiddenField)e.Item.FindControl("hfDeveloperId");
                int devId = int.Parse(hfDeveloperId.Value);
                var developerCategories = DbManager.GetDevCategories(devId);
                if (developerCategories.Success)
                {
                    if (developerCategories.Data.Count == 0)
                    {
                        List<string> emptyCategories = new List<string>();
                        emptyCategories.Add("Kayıtlı kategori yok.");
                        rptDeveloperCategories.DataSource = emptyCategories;
                    }
                    else
                    {
                        rptDeveloperCategories.DataSource = developerCategories.Data.Select(d => d.CategoryName);
                    }
                    rptDeveloperCategories.DataBind();
                }
                else
                {
                    InfoLabel(developerCategories.Message);
                }
                var devArticles = DbManager.GetDevArticles(devId);
                if (devArticles.Success)
                {
                    if (devArticles.Data.Count == 0)
                    {
                        List<string> emptyArticles = new List<string>();
                        emptyArticles.Add("Kayıtlı yazı yok.");
                        rptDeveloperArticles.DataSource = emptyArticles;
                    }
                    else
                    {
                        rptDeveloperArticles.DataSource = devArticles.Data.Select(d => d.ArticleTitle);
                    }
                    rptDeveloperArticles.DataBind();
                }
                else
                {
                    InfoLabel(devArticles.Message);
                }
            }
        }

        protected void rptDevelopers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int devID = int.Parse((string)(e.CommandArgument));
            var dev = DbManager.GetDeveloperByID(devID);
            if (e.CommandName == "Edit")
            {
                
                if (dev.Success)
                {
                    txtDevName.Text = dev.Data.FirstName;
                    hfDeveloperId.Value = dev.Data.ObjectID.ToString();
                    txtDevLastname.Text = dev.Data.LastName;
                    txtDevUsername.Text = dev.Data.UserName;
                    //yazdığı kategoriler
                    //burda hata var!!!!
                    List<int> test1 = dev.Data.Categories.Select(c => c.ObjectID).ToList();

                    //foreach (var category in dev.Data.Categories)
                    //{
                    //    test1.Add(category.ObjectID);
                    //}
                    List<int> allCatIds = (List<int>)DbManager.GetCategories().Data.Select(c => c.ObjectID).ToList();
                    //yazısı olan kategori idler
                    if (dev.Data.Articles.Count != 0)
                    {
                        List<int> test2 = (List<int>)dev.Data.Articles.SelectMany(a => a.Categories.Select(c => c.ObjectID)).ToList();
                        foreach (int item in test2)
                        {
                            allCatIds.Remove(item);
                            test1.Remove(item);
                        }
                    }
                    LbEditCategories.DataSource = DbManager.GetCategoriesByMultiIds(allCatIds).Data.Select(c => new { c.CategoryName, c.ObjectID});
                    LbEditCategories.DataTextField = "CategoryName";
                    LbEditCategories.DataValueField = "ObjectID";
                    foreach (int item in test1)
                    {
                        LbEditCategories.SelectedValue = item.ToString();
                    }
                    LbEditCategories.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDevModal();", true);
                }
            }
            else
            {
                if (dev.Data.Articles.Count == 0)
                {
                    DeleteDeveloper(devID);
                }
                else
                {
                    InfoLabel(Messages.ErrorDeleteDeveloperDueToArticles);
                }
            }
        }

        private void DeleteDeveloper(int devID)
        {
            var res = DbManager.DeleteDeveloper(devID);
            if (res.Success)
            {
                Application["Info"] = res.Message;
                Response.Redirect("Developers.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                InfoLabel(res.Message);
            }
        }

        protected void btnModalSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine(txtDevUsername.Text);
            Console.WriteLine(txtDevLastname.Text);
            Console.WriteLine(txtDevName.Text);
            Console.WriteLine(LbEditCategories.Items);
            List<int> selectedCatIds = new List<int>();
            foreach (ListItem item in LbEditCategories.Items)
            {
                if (item.Selected)
                {
                    selectedCatIds.Add(int.Parse(item.Value));
                }
            }
            var res = DbManager.UpdateDeveloper(int.Parse(hfDeveloperId.Value), txtDevUsername.Text, txtDevName.Text, txtDevLastname.Text, selectedCatIds);
            if (res.Success)
            {
                Application["Info"] = res.Message;
                Response.Redirect("Developers.aspx");
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                InfoLabel(res.Message);
            }
        }
    }
}