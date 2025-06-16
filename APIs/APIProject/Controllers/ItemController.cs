using APIProject.Data.DTOs;
using APIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : Controller
    {
        private readonly AppDBContext _context;
        public ItemController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if(item != null) return Ok(item);
            return NotFound(new { Error = $"The Item with Id {id} Not Found" });    
        }
        [HttpGet("itemsWithCategoryId/{categoryId}")]
        public async Task<IActionResult> GetItemsByCategoryId(int categoryId)
        {
            if (categoryId <= 0) return BadRequest(new { Error = "The Id Should be equal or more than 1" });

            var items = await _context.Items.Where(x => x.CategoryId == categoryId).ToListAsync();
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] ItemDTO itemDTO)
        {
            if (itemDTO.Image.Length < 0 || !await _context.Categories.AnyAsync(i => i.Id == itemDTO.CategoryId)) return BadRequest();
            using(var stream = new MemoryStream())
            {
                await itemDTO.Image.CopyToAsync(stream);
                var item = new Item
                {
                    ItemName = itemDTO.ItemName,
                    ItemDescription = itemDTO.ItemDescription,
                    ItemPrice = itemDTO.ItemPrice,
                    Image = stream.ToArray(),
                    CategoryId = itemDTO.CategoryId,
                };
                await _context.Items.AddAsync(item);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Detail", new { id = item.Id }, item);
            }
        }

    }
}
