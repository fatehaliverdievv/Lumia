using Lumia.DAL;
using Lumia.Models;
using Lumia.Utilies.Extension;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env{ get; }

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.Include(e=>e.Position).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Positions=new SelectList(_context.Positions,nameof(Position.Id),nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId))
            {
                ModelState.AddModelError("PositionId", "bele position yoxdu");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            string result = employeeVM.Image.CheckValidate("image/", 900);
            if (result.Length > 0)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                ModelState.AddModelError("Image", result);
                return View();
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                PositionId = employeeVM.PositionId,
                InstagramUrl = employeeVM.InstagramUrl,
                TwitterLink = employeeVM.TwitterLink,
                FacebookUrl = employeeVM.FacebookUrl,
                LinkedinLink = employeeVM.LinkedinLink,
                ImgUrl = employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img")),
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null) return NotFound();
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                Surname = employee.Surname,
                PositionId = employee.PositionId,
                InstagramUrl = employee.InstagramUrl,
                TwitterLink = employee.TwitterLink,
                FacebookUrl = employee.FacebookUrl,
                LinkedinLink = employee.LinkedinLink,
            };
            ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM employeeVM)
        {
            if (id == null) return BadRequest();
            var employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null) return NotFound();
            if (!_context.Positions.Any(p => p.Id == employeeVM.PositionId))
            {
                ModelState.AddModelError("PositionId", "bele position yoxdu");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            if (employeeVM.Image != null)
            {
                string result = employeeVM.Image.CheckValidate("image/", 900);
                if (result.Length > 0)
                {
                    ViewBag.Positions = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
                    ModelState.AddModelError("Image", result);
                    return View();
                }
                string image= employeeVM.Image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"));
                employee.ImgUrl.DeleteFile(_env.WebRootPath, "assets/img");
                employee.ImgUrl= image;
            }
            employee.Name = employeeVM.Name;
            employee.Surname = employeeVM.Surname;
            employee.PositionId = employeeVM.PositionId;
            employee.InstagramUrl = employee.InstagramUrl;
            employee.TwitterLink = employeeVM.TwitterLink;
            employee.FacebookUrl = employeeVM.FacebookUrl;
            employee.LinkedinLink = employeeVM.LinkedinLink;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var employee = await _context.Employees.FirstOrDefaultAsync(p => p.Id == id);
            if (employee == null) return NotFound();
            employee.ImgUrl.DeleteFile(_env.WebRootPath, "assets/img");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
