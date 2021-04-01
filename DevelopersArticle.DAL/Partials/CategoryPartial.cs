using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.DAL
{
    public partial class Category
    {

    }
    public partial class DevelopersArticlesEntities
    {
        public void AddCategory(Category category)
        {
            Categories.Add(category);
        }
        public void SoftDeleteCategory(Category category)
        {
            var deletedItem = Categories.Find(category.ObjectID);
            deletedItem.IsDeleted = true;
            deletedItem.DeletedDate = DateTime.Now;

        }
        public void UpdateCategory(Category category)
        {
            var UpdatedEntity = Entry(category);
            UpdatedEntity.State = EntityState.Modified;
        }

        public List<Category> GetAllCategories()
        {
            return Categories.Where(c => c.IsDeleted == false).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return Categories.SingleOrDefault(c => c.IsDeleted == false && c.ObjectID == categoryId);
        }
    }
}
