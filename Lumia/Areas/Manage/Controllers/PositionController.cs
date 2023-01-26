using Lumia.DAL;
using Lumia.Models;
using Lumia.Utilies.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        AppDbContext _context { get; }

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Positions.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var position = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (position == null) return NotFound();
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if(_context.Positions.Any(p=>p.Name==position.Name)) 
            {
                ModelState.AddModelError("Name", "This position already exsist");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            Position newposition = new Position
            {
                Name = position.Name,
            };
            await _context.Positions.AddAsync(newposition);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int? id) 
        {
            if (id == null) return BadRequest();
            var position = await _context.Positions.FirstOrDefaultAsync(p=>p.Id==id);
            if (position == null) return NotFound();
            return View(position);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Position position)
        {
            if (id == null) return BadRequest();
            var existedposition = await _context.Positions.FirstOrDefaultAsync(p => p.Id == id);
            if (existedposition == null) return NotFound();
            if(existedposition.Name!=position.Name)
            {
                if (_context.Positions.Any(p => p.Name == position.Name))
                {
                    ModelState.AddModelError("Name", "This position already exsist");
                }
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            existedposition.Name= position.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
