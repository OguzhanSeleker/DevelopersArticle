using DevelopersArticle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.ConsoleTest
{
    public class Program : BaseProgram
    {

        public static void Main()
        {
            byte[] a = new byte[3];
            a[0] = 0;
            var catList = new List<Category>();
            Category category = new Category()
            {
                CategoryName = "Entity Framework",
                CreatedDate = DateTime.Now,
            };
            
            catList.Add(category);
            Developer developer = new Developer()
            {
                FirstName = "Arif Umut",
                LastName = "Sepetçi",
                CreatedDate = DateTime.Now,
                UserName = "sepetci16",
                
                Categories = catList

            };
            DbInstance.AddCategory(category);
            DbInstance.SaveChanges();
            DbInstance.AddDeveloper(developer);
            DbInstance.SaveChanges();

        }
    }
}
