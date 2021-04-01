using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.DAL
{
    public partial class Developer
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public partial class DevelopersArticlesEntities
    {
        public void AddDeveloper(Developer developer)
        {
            Developers.Add(developer);
        }
        public void SoftDeleteDeveloper(int developerId)
        {
            var deletedItem = Developers.Find(developerId);
            deletedItem.IsDeleted = true;
            deletedItem.DeletedDate = DateTime.Now;
            Entry(deletedItem).State = EntityState.Modified;
        }
        public void UpdateDeveloper(Developer developer)
        {
            var UpdatedEntity = Entry(developer);
            UpdatedEntity.State = EntityState.Modified;
        }

        public Developer GetDeveloperById(int developerId)
        {
            return Developers.SingleOrDefault(d => d.IsDeleted == false && d.ObjectID == developerId);
        }
        public List<Developer> GetAllDevelopers()
        {
            return Developers.Where(d => d.IsDeleted == false).ToList();
        }
        public List<Developer> GetDevelopersByCategory(int categoryId)
        {
            return Categories.Where(d => d.ObjectID == categoryId).SelectMany(d => d.Developers).ToList();
        }

        public List<Category> GetDeveloperCategoriesbyDevId(int developerId)
        {
            return Developers.Where(d => d.ObjectID == developerId).SelectMany(d => d.Categories).ToList();
        }
        
    }
}
