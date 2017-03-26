using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Models;

namespace SacramentPlanner.Controllers
{
    public class HymnsController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public HymnsController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: Hymns
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.Hymn.Include(h => h.FkHymnTypeNavigation);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: Hymns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn
                .Include(h => h.FkHymnTypeNavigation)
                .SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }

            return View(hymn);
        }

        // GET: Hymns/Create
        public IActionResult Create()
        {
            ViewData["FkHymnType"] = new SelectList(_context.HymnType, "HymnTypeId", "HymnType1");
            return View();
        }

        // POST: Hymns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HymnId,HymnTitle,HymnNum,FkHymnType")] Hymn hymn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hymn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FkHymnType"] = new SelectList(_context.HymnType, "HymnTypeId", "HymnType1", hymn.FkHymnType);
            return View(hymn);
        }

        // GET: Hymns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn.SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }
            ViewData["FkHymnType"] = new SelectList(_context.HymnType, "HymnTypeId", "HymnType1", hymn.FkHymnType);
            return View(hymn);
        }

        // POST: Hymns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HymnId,HymnTitle,HymnNum,FkHymnType")] Hymn hymn)
        {
            if (id != hymn.HymnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hymn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HymnExists(hymn.HymnId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["FkHymnType"] = new SelectList(_context.HymnType, "HymnTypeId", "HymnType1", hymn.FkHymnType);
            return View(hymn);
        }

        // GET: Hymns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymn = await _context.Hymn
                .Include(h => h.FkHymnTypeNavigation)
                .SingleOrDefaultAsync(m => m.HymnId == id);
            if (hymn == null)
            {
                return NotFound();
            }

            return View(hymn);
        }

        // POST: Hymns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hymn = await _context.Hymn.SingleOrDefaultAsync(m => m.HymnId == id);
            _context.Hymn.Remove(hymn);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HymnExists(int id)
        {
            return _context.Hymn.Any(e => e.HymnId == id);
        }
    }
}
