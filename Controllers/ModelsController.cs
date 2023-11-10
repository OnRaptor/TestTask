using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Data.Models;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class ModelsController : Controller
    {
        private readonly DataContext _context;

        public ModelsController(DataContext context)
        {
            _context = context;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            if (_context.Models == null)
                return Problem("Entity set 'DataContext.Models'  is null.");
            else
                return View(_context.Brands.ToList().Select
                        (brand => new ModelGroup 
                            {
                                Brand = brand,
                                Models = _context.Models.ToList().Where(model => model.Brand.Id == brand.Id).ToList()
                            })
                        );
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(model => model.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Models/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = (await _context.Brands.ToListAsync())
                .Select(i => new SelectListItem(i.Name, i.Id.ToString()));
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BrandId,Active")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Brands = (await _context.Brands.ToListAsync())
                .Select(i => new SelectListItem(i.Name, i.Id.ToString()));
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BrandId,Active")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Models == null)
            {
                return Problem("Entity set 'DataContext.Models'  is null.");
            }
            var model = await _context.Models.FindAsync(id);
            if (model != null)
            {
                _context.Models.Remove(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return (_context.Models?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
