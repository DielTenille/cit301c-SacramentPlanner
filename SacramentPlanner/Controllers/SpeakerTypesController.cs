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
    public class SpeakerTypesController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SpeakerTypesController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: SpeakerTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpeakerType.ToListAsync());
        }

        // GET: SpeakerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerType = await _context.SpeakerType
                .SingleOrDefaultAsync(m => m.SpeakerTypeId == id);
            if (speakerType == null)
            {
                return NotFound();
            }

            return View(speakerType);
        }

        // GET: SpeakerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpeakerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerTypeId,SpeakerType1")] SpeakerType speakerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speakerType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(speakerType);
        }

        // GET: SpeakerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerType = await _context.SpeakerType.SingleOrDefaultAsync(m => m.SpeakerTypeId == id);
            if (speakerType == null)
            {
                return NotFound();
            }
            return View(speakerType);
        }

        // POST: SpeakerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerTypeId,SpeakerType1")] SpeakerType speakerType)
        {
            if (id != speakerType.SpeakerTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerTypeExists(speakerType.SpeakerTypeId))
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
            return View(speakerType);
        }

        // GET: SpeakerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerType = await _context.SpeakerType
                .SingleOrDefaultAsync(m => m.SpeakerTypeId == id);
            if (speakerType == null)
            {
                return NotFound();
            }

            return View(speakerType);
        }

        // POST: SpeakerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakerType = await _context.SpeakerType.SingleOrDefaultAsync(m => m.SpeakerTypeId == id);
            _context.SpeakerType.Remove(speakerType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SpeakerTypeExists(int id)
        {
            return _context.SpeakerType.Any(e => e.SpeakerTypeId == id);
        }
    }
}
