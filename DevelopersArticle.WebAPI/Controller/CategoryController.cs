using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DevelopersArticle.WebAPI.Controller
{
    
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var res = DbManager.GetCategories();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

        [HttpGet]
        public IHttpActionResult GetbyId(int id)
        {
            var res = DbManager.GetCategoryById(id);
            if (res.Success)
            {
                return Ok(res.Data.CategoryName);
            }
            return BadRequest(res.Message);
        }
    }
}