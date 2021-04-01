//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevelopersArticle.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Article
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.Categories = new HashSet<Category>();
        }
    
        public int ObjectID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public int WriterId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsModified { get; set; }
        public byte[] ArticlePictureURL { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Developer Developer { get; set; }
    }
}