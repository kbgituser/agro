using Microsoft.AspNetCore.Mvc;
using PlatF.Model.Data;
using PlatF.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlatF.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<CategoriesApiController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {            
            return _context.Categories;
        }

        // GET api/<CategoriesApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("children/{id}")]
        public IQueryable<Category> GetSubCategories (int id)
        {
            
            var subCategories = _context.Categories.Where(x => x.ParentCategoryId.HasValue
            && x.ParentCategoryId.Value == id);
            return subCategories;
        }

        // POST api/<CategoriesApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriesApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
