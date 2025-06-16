using APIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var data = await _context.Categories.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null) 
            {
                // Explicit Load await _context.Entry(category).Collection(i => i.Items).LoadAsync();
                // Eager Load => await _context.Categories.Include(i => i.Items).FirstOrDefaultAsync(x => x.Id == id)
                return Ok(category);
            } 
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Category cate)
        {
            await _context.Categories.AddAsync(cate);
            await _context.SaveChangesAsync();
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Category newcategory)
        {
            if (id <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category != null)
            {
                category.Name = newcategory.Name;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });
            
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            
            return NotFound();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdate(int id, Category cate)
        {
            if (id <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(category != null)
            {
                category.Name = cate.Name;
                _context.Update(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            return NotFound();
        }
    }
}
