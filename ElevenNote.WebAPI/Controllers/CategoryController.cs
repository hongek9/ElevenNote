using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var categoryService = new CategoryService(userId);
            return categoryService;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryCreate model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.CreateCategory(model)) return InternalServerError();

            return Ok();   
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = CreateCategoryService();

            var categories = service.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var service = CreateCategoryService();

            var category = service.GetCategoryById(id);

            if (category is null) return BadRequest("Not category with that ID.");

            return Ok(category);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody] CategoryDetail category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateCategoryService();

            if (!service.EditCategory(category)) return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateCategoryService();

            if (!service.DeleteCategory(id)) return InternalServerError();

            return Ok();
        }
    }
}
