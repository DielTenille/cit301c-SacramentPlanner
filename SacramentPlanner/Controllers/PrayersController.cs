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
    public class PrayersController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public PrayersController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: Prayers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.Prayer.Include(p => p.FkPrayerTypeNavigation).Include(p => p.FkWardMember);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: Prayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer = await _context.Prayer
                .Include(p => p.FkPrayerTypeNavigation)
                .Include(p => p.FkWardMember)
                .SingleOrDefaultAsync(m => m.PrayerId == id);
            if (prayer == null)
            {
                return NotFound();
            }

            return View(prayer);
        }

        // GET: Prayers/Create
        public IActionResult Create()
        {
            ViewData["FkPrayerType"] = new SelectList(_context.PrayerType, "PrayerTypeId", "TypePrayer");
            ViewData["FkWardMemberId"] = new SelectList(_context.WardMember, "WardMemberId", "Fname");
            return View();
        }

        // POST: Prayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrayerId,FkPrayerType,PrayerDate,FkWardMemberId")] Prayer prayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prayer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FkPrayerType"] = new SelectList(_context.PrayerType, "PrayerTypeId", "TypePrayer", prayer.FkPrayerType);
            ViewData["FkWardMemberId"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", prayer.FkWardMemberId);
            return View(prayer);
        }

        // GET: Prayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer = await _context.Prayer.SingleOrDefaultAsync(m => m.PrayerId == id);
            if (prayer == null)
            {
                return NotFound();
            }
            ViewData["FkPrayerType"] = new SelectList(_context.PrayerType, "PrayerTypeId", "TypePrayer", prayer.FkPrayerType);
            ViewData["FkWardMemberId"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", prayer.FkWardMemberId);
            return View(prayer);
        }

        // POST: Prayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrayerId,FkPrayerType,PrayerDate,FkWardMemberId")] Prayer prayer)
        {
            if (id != prayer.PrayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrayerExists(prayer.PrayerId))
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
            ViewData["FkPrayerType"] = new SelectList(_context.PrayerType, "PrayerTypeId", "TypePrayer", prayer.FkPrayerType);
            ViewData["FkWardMemberId"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", prayer.FkWardMemberId);
            return View(prayer);
        }

        // GET: Prayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prayer = await _context.Prayer
                .Include(p => p.FkPrayerTypeNavigation)
                .Include(p => p.FkWardMember)
                .SingleOrDefaultAsync(m => m.PrayerId == id);
            if (prayer == null)
            {
                return NotFound();
            }

            return View(prayer);
        }

        // POST: Prayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prayer = await _context.Prayer.SingleOrDefaultAsync(m => m.PrayerId == id);
            _context.Prayer.Remove(prayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PrayerExists(int id)
        {
            return _context.Prayer.Any(e => e.PrayerId == id);
        }
    }
}
