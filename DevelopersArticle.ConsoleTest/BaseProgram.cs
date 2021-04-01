using DevelopersArticle.DAL;

namespace DevelopersArticle.ConsoleTest
{
    public class BaseProgram
    {
        private static DevelopersArticlesEntities db;

        public static DevelopersArticlesEntities DbInstance
        {
            get
            {
                if (db == null)
                {
                    db = new DevelopersArticlesEntities();
                }
                return db;
            }
        }


    }
}
