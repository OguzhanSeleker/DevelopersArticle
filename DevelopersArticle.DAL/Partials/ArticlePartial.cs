﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.DAL
{
    public partial class Article
    {
        
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
            var deletedItem = Set<Article>().Find(article);
            deletedItem.IsDeleted = true;
            deletedItem.DeletedDate = DateTime.Now;
            deletedItem.ModifiedDate = DateTime.Now;
            Entry(deletedItem).State = EntityState.Modified;
        }
        public void UpdateArticle(Article article)
        {
            var UpdatedEntity = Entry(article);
            UpdatedEntity.State = EntityState.Modified;
        }

        public Article GetArticleById(int articleId)
        {
            return Articles.SingleOrDefault(a => a.IsDeleted == false && a.ObjectID == articleId);
        }

        public List<Article> GetArticlesByCategoryId(int categoryId)
        {

            return Categories.Where(c => c.ObjectID == categoryId).SelectMany(a => a.Articles).ToList();
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
