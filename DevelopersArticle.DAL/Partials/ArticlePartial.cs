using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.DAL
{
    public partial class Article
    {
        public string ImageUrl
        {
            get
            {
                string image = Convert.ToBase64String(ArticlePictureURL, 0, ArticlePictureURL.Length);
                return "data:image/png;base64," + image;
            }
        }
        public string MakaleOzet
        {
            get
            {
                string ozet = "";
                var listOzet = ArticleContent.Split(' ').Take(22).ToList();
                foreach (var word in listOzet)
                {
                    ozet += word + " ";
                }
                ozet += "...";
                return ozet;
            }

        }
    }
    public partial class DevelopersArticlesEntities
    {

        public void AddArticle(Article article)
        {
            var addedEntity = Entry(article);
            addedEntity.State = EntityState.Added;
        }
        public void SoftDeleteArticle(Article article)
        {
            var deletedItem = Set<Article>().Find(article.ObjectID);
            deletedItem.IsDeleted = true;
            deletedItem.DeletedDate = DateTime.Now;
            deletedItem.ModifiedDate = DateTime.Now;
        }
        public void UpdateArticle(Article article)
        {
            var UpdatedEntity = Set<Article>().Find(article.ObjectID);
            UpdatedEntity.WriterId = article.WriterId;
            UpdatedEntity.ModifiedDate = DateTime.Now;
            UpdatedEntity.IsModified = true;
            UpdatedEntity.ArticleTitle = article.ArticleTitle;
            UpdatedEntity.ArticlePictureURL = article.ArticlePictureURL;
            UpdatedEntity.ArticleContent = article.ArticleContent;
            UpdatedEntity.Categories = article.Categories;
        }

        public Article GetArticleById(int articleId)
        {
            return Articles.SingleOrDefault(a => a.IsDeleted == false && a.ObjectID == articleId);
        }

        public List<Article> GetArticlesByCategoryId(int categoryId)
        {
            return Categories.Where(c => c.ObjectID == categoryId && c.IsDeleted == false).SelectMany(a => a.Articles.Where(d => d.IsDeleted == false)).ToList();
        }

        public List<Article> GetAllArticles()
        {
            return Articles.Where(a => a.IsDeleted == false).OrderBy(a => a.ModifiedDate).ToList();
        }
        public List<Article> GetArticlesByWriterId(int writerId)
        {
            return Articles.Where(a => a.WriterId == writerId && a.IsDeleted == false).ToList();
        }
        public List<Article> GetDevelopersArticles(int developerId)
        {
            return Articles.Where(a => a.IsDeleted == false && a.WriterId == developerId).ToList();
        }
    }
}
