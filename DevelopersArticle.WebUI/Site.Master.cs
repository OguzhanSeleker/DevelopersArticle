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
    public partial class Site : MasterPage
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

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetSidebarInfo();
            }
        }
        private void GetSidebarInfo()
        {
            var categories = DbManager.GetCategories();
            if (categories.Success)
            {
                rpKategori.DataSource = categories.Data.Select(c => new { c.ObjectID, c.CategoryName });
                rpKategori.DataBind();
            }
            ImgSidebarHeader.ImageUrl = "Images/male_icon.jpg";
        }

        protected void rpKategori_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int catId = int.Parse((string)(e.CommandArgument));
            Response.Redirect("Default.aspx?Category=" + catId);
        }
    }
}