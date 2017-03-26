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
    public class HymnTypesController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public HymnTypesController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: HymnTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HymnType.ToListAsync());
        }

        // GET: HymnTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymnType = await _context.HymnType
                .SingleOrDefaultAsync(m => m.HymnTypeId == id);
            if (hymnType == null)
            {
                return NotFound();
            }

            return View(hymnType);
        }

        // GET: HymnTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HymnTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HymnTypeId,HymnType1")] HymnType hymnType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hymnType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hymnType);
        }

        // GET: HymnTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymnType = await _context.HymnType.SingleOrDefaultAsync(m => m.HymnTypeId == id);
            if (hymnType == null)
            {
                return NotFound();
            }
            return View(hymnType);
        }

        // POST: HymnTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HymnTypeId,HymnType1")] HymnType hymnType)
        {
            if (id != hymnType.HymnTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hymnType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HymnTypeExists(hymnType.HymnTypeId))
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
            return View(hymnType);
        }

        // GET: HymnTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hymnType = await _context.HymnType
                .SingleOrDefaultAsync(m => m.HymnTypeId == id);
            if (hymnType == null)
            {
                return NotFound();
            }

            return View(hymnType);
        }

        // POST: HymnTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hymnType = await _context.HymnType.SingleOrDefaultAsync(m => m.HymnTypeId == id);
            _context.HymnType.Remove(hymnType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HymnTypeExists(int id)
        {
            return _context.HymnType.Any(e => e.HymnTypeId == id);
        }
    }
}
