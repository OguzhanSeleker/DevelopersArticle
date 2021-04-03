using DevelopersArticle.BLL.Utilities.Results;
using DevelopersArticle.DAL;
using System.Collections.Generic;

namespace DevelopersArticle.BLL
{
    public interface IService
    {
        IResult AddArticle(string ArticleTitle, byte[] ImageBytes, string ArticleContent, List<int> CategoryIds, int writerID);
        IDataResult<List<Category>> GetCategories();
        IDataResult<List<Developer>> GetDevelopers();
        IResult AddCategory(string categoryName);
        IDataResult<List<string>> GetDevelopersFullNameInCategory(int categoryId);
        IDataResult<List<Article>> GetArticlesInCategory(int categoryId);
        IDataResult<Category> GetCategoryById(int categoryName);
        IResult UpdateCategoryName(int categoryId, string categoryName);
        IResult DeleteCategory(int categoryId);
        IResult AddDeveloper(string developerName, string developerLastname, string username, List<int> categoryIds);
        IDataResult<List<Category>> GetDevCategories(int developerId);
        IDataResult<List<Article>> GetDevArticles(int developerId);
        IDataResult<Developer> GetDeveloperByID(int developerId);
        IDataResult<List<Category>> GetCategoriesByMultiIds(List<int> categoryIds);
        IResult UpdateDeveloper(int devId,string userName, string firstName, string lastName, List<int> categoryIDs);
        IResult DeleteDeveloper(int devID);
        IDataResult<List<Article>> GetAllArticles();
        IDataResult<List<Article>> GetArticlesByCategoryId(int categoryId);
        IDataResult<Article> GetArticleById(int articleId);
        IResult AddComment(int articleId, int developerId, string commentContent);
        IResult DeleteArticle(int articleId);
        IResult UpdateArticle(int articleId, string ArticleTitle, byte[] ImageBytes, string ArticleContent, List<int> CategoryIds, int writerID);
        IDataResult<Comment> GetCommentById(int commentId);
        IResult DeleteComment(int commentId);
    }
}