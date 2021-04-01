﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.DAL
{
    public partial class Comment
    {

    }
    public partial class DevelopersArticlesEntities
    {
        public void AddComment(Comment comment)
        {
            var addedEntity = Entry(comment);
            addedEntity.State = EntityState.Added;
        }
        public void SoftDeleteComment(Comment comment)
        {
            var deletedItem = Set<Comment>().Find(comment);
            deletedItem.IsDeleted = true;
            deletedItem.DeletedDate = DateTime.Now;
            deletedItem.ModifiedDate = DateTime.Now;
            Entry(deletedItem).State = EntityState.Modified;

        }
        public void UpdateComment(Comment Comment)
        {
            var UpdatedEntity = Entry(Comment);
            UpdatedEntity.State = EntityState.Modified;
        }
        
    }
}