using DevelopersArticle.BLL.Utilities.Constants;
using DevelopersArticle.BLL.Utilities.Results;
using DevelopersArticle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.BLL.Concrete
{
    public class DBManager : BaseSingleton, IService
    {
        public IResult AddArticle(string ArticleTitle, byte[] ImageBytes, string ArticleContent, List<int> CategoryIds, int writerID)
        {
            List<Category> categories = new List<Category>();

            foreach (var categoryId in CategoryIds)
            {
                categories.Add(DbInstance.GetCategoryById(categoryId));
            }

            Article article = new Article
            {
                ArticleTitle = ArticleTitle,
                ArticleContent = ArticleContent,
                ArticlePictureURL = ImageBytes,
                Developer = DbInstance.GetDeveloperById(writerID),
                WriterId = writerID,
                Categories = categories,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            try
            {
                DbInstance.AddArticle(article);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SucceededArticle);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.UnsucceededArticle);
            }
        }

        public IResult AddCategory(string categoryName)
        {
            Category category = new Category
            {
                CategoryName = categoryName,
                CreatedDate = DateTime.Now
            };
            try
            {
                DbInstance.AddCategory(category);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SucceededCategory);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.UnSucceededCategory);
            }
        }

        public IResult AddDeveloper(string developerName, string developerLastname, string username, List<int> categoryIds)
        {
            List<Category> categories = new List<Category>();
            foreach (int categoryId in categoryIds)
            {
                Category cat = DbInstance.GetCategoryById(categoryId);
                categories.Add(cat);
            }
            Developer developer = new Developer
            {
                Categories = categories,
                CreatedDate = DateTime.Now,
                FirstName = developerName,
                LastName = developerLastname,
                UserName = username,
            };
            try
            {
                DbInstance.AddDeveloper(developer);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessAddDeveloper);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorAddDeveloper);
            }

        }

        public IResult DeleteCategory(int categoryId)
        {
            try
            {
                var category = DbInstance.GetCategoryById(categoryId);
                DbInstance.SoftDeleteCategory(category);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessDeleteCategory);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorDeleteCategory);
            }
        }

        public IDataResult<List<Article>> GetArticlesInCategory(int categoryId)
        {
            try
            {
                return new SuccessDataResult<List<Article>>(DbInstance.GetArticlesByCategoryId(categoryId));
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<Article>>(Messages.ErrorArticlesInCategory);
            }
        }

        public IDataResult<List<Category>> GetCategories()
        {
            try
            {
                return new SuccessDataResult<List<Category>>(DbInstance.GetAllCategories());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Category>>(Messages.NotListedCategories);
            }
        }

        public IDataResult<List<Category>> GetCategoriesByMultiIds(List<int> categoryIds)
        {
            List<Category> categories = new List<Category>();
            foreach (int item in categoryIds)
            {
                categories.Add(DbInstance.GetCategoryById(item));
            }
            return new SuccessDataResult<List<Category>>(categories);
        }

        public IDataResult<Category> GetCategoryById(int categoryName)
        {
            try
            {
                return new SuccessDataResult<Category>(DbInstance.GetCategoryById(categoryName));
            }
            catch (Exception)
            {
                return new ErrorDataResult<Category>();
            }
        }

        public IDataResult<List<Article>> GetDevArticles(int developerId)
        {
            try
            {
                List<Article> articles = DbInstance.GetArticlesByWriterId(developerId);
                return new SuccessDataResult<List<Article>>(articles);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Article>>(Messages.ErrorGetArticles);
            }
        }

        public IDataResult<List<Category>> GetDevCategories(int developerId)
        {
            try
            {
                List<Category> categories = DbInstance.GetDeveloperCategoriesbyDevId(developerId);
                return new SuccessDataResult<List<Category>>(categories);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<Category>>(Messages.ErrorGetCategories);
            }
        }

        public IDataResult<Developer> GetDeveloperByID(int developerId)
        {
            try
            {
                Developer developer = DbInstance.GetDeveloperById(developerId);
                return new SuccessDataResult<Developer>(developer);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Developer>(Messages.Error);
            }
        }

        public IDataResult<List<Developer>> GetDevelopers()
        {
            try
            {
                return new SuccessDataResult<List<Developer>>(DbInstance.GetAllDevelopers());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Developer>>(Messages.NotListedDevelopers);
            }
        }

        public IDataResult<List<string>> GetDevelopersFullNameInCategory(int categoryId)
        {
            try
            {
                List<string> developersFullname = DbInstance.GetDevelopersByCategory(categoryId).Select(s => s.FullName).ToList();
                if (developersFullname.Count == 0)
                    developersFullname.Add("Kategoride yazan yok.");
                return new SuccessDataResult<List<string>>(developersFullname);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<string>>(Messages.ErrorDevelopersFullname);
            }
        }

        public IResult UpdateCategoryName(int categoryId, string categoryName)
        {
            try
            {
                Category category = DbInstance.GetCategoryById(categoryId);
                category.CategoryName = categoryName;
                DbInstance.UpdateCategory(category);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessUpdateCategory);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorUpdateCategory);
            }
        }

        public IResult UpdateDeveloper(int devId, string userName, string firstName, string lastName, List<int> categoryIDs)
        {
            try
            {
                List<Category> categories = new List<Category>();

                Developer dev = DbInstance.GetDeveloperById(devId);
                if (dev.Articles.Count != 0)
                {
                    foreach (var article in dev.Articles.Where(a => a.IsDeleted == false).ToList())
                    {
                        foreach (var artCat in article.Categories)
                        {
                            categories.Add(artCat);
                        } 
                    }
                }

                if (categoryIDs.Count != 0)
                {
                    foreach (var carId in categoryIDs)
                    {
                        categories.Add(DbInstance.GetCategoryById(carId));
                    }
                }
                dev.Categories.Clear();
                foreach (var newCat in categories)
                {
                    dev.Categories.Add(newCat);
                }
                dev.FirstName = firstName;
                dev.UserName = userName;
                dev.LastName = lastName;
                DbInstance.UpdateDeveloper(dev);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessUpdateDeveloper);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorUpdateDeveloper);
            }

        }

        public IResult DeleteDeveloper(int devID)
        {
            try
            {
                DbInstance.SoftDeleteDeveloper(devID);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessDeleteDeveloper);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorDeleteDeveloper);
            }
        }

        public IDataResult<List<Article>> GetAllArticles()
        {
            try
            {
                var articleList = DbInstance.GetAllArticles();
                return new SuccessDataResult<List<Article>>(articleList);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Article>>(Messages.ErrorGetAllArticles);
            }
        }

        public IDataResult<List<Article>> GetArticlesByCategoryId(int categoryId)
        {
            try
            {
                var articleListByCategory = DbInstance.GetArticlesByCategoryId(categoryId);
                return new SuccessDataResult<List<Article>>(articleListByCategory);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Article>>(Messages.ErrorGetAllArticles);
            }
        }

        public IDataResult<Article> GetArticleById(int articleId)
        {
            try
            {
                Article article = DbInstance.GetArticleById(articleId);
                if (article == null)
                {
                    return new ErrorDataResult<Article>(Messages.ErrorGetArticle);
                }
                return new SuccessDataResult<Article>(article);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Article>(Messages.ErrorGetArticle);
            }
        }

        public IResult AddComment(int articleId, int developerId, string commentContent)
        {
            try
            {
                Comment comment = new Comment
                {
                    ArticleId = articleId,
                    WriterId = developerId,
                    CommentContent = commentContent,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                };

                DbInstance.AddComment(comment);
                DbInstance.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorAddComment);
            }

        }

        public IResult DeleteArticle(int articleId)
        {
            var article = DbInstance.GetArticleById(articleId);
            try
            {
                DbInstance.SoftDeleteArticle(article);

                if (article.Comments.Count != 0)
                {
                    foreach (var comment in article.Comments)
                    {
                        DbInstance.SoftDeleteComment(comment);
                    }

                }
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessDeleteArticle);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.ErrorDeleteArticle + e.Message);
            }
        }

        public IResult UpdateArticle(int articleId, string ArticleTitle, byte[] ImageBytes, string ArticleContent, List<int> CategoryIds, int writerID)
        {
            try
            {
                Article article = DbInstance.GetArticleById(articleId);
                article.ArticleContent = ArticleContent;
                article.ArticlePictureURL = ImageBytes;
                article.ArticleTitle = ArticleTitle;
                article.Categories.Clear();
                foreach (var item in CategoryIds)
                {
                    var cat = DbInstance.GetCategoryById(item);
                    article.Categories.Add(cat);
                }
                article.WriterId = writerID;
                DbInstance.UpdateArticle(article);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessUpdateArticle);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.ErrorUpdateArticle + e.Message);
            }
        }

        public IDataResult<Comment> GetCommentById(int commentId)
        {
            try
            {
                var comment = DbInstance.GetCommentById(commentId);
                return new SuccessDataResult<Comment>(comment);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Comment>();
            }
        }

        public IResult DeleteComment(int commentId)
        {
            try
            {
                Comment comment = DbInstance.GetCommentById(commentId);
                DbInstance.SoftDeleteComment(comment);
                DbInstance.SaveChanges();
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.ErrorDeleteComment + e.Message);
            }
        }

        public IResult UpdateComment(int commentId, string commentContent, int writerId)
        {
            try
            {
                Comment comment = DbInstance.GetCommentById(commentId);
                comment.CommentContent = commentContent;
                comment.WriterId = writerId;
                comment.IsModified = true;
                comment.ModifiedDate = DateTime.Now;
                DbInstance.UpdateComment(comment);
                DbInstance.SaveChanges();
                return new SuccessResult(Messages.SuccessUpdateComment);
            }
            catch (Exception e)
            {
                return new ErrorResult(Messages.ErrorUpdateComment + e.Message);
            }
            
        }
    }
}
