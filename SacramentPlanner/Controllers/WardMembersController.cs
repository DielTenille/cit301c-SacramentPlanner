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
    public class WardMembersController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public WardMembersController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: WardMembers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.WardMember.Include(w => w.FkCalling);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: WardMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardMember = await _context.WardMember
                .Include(w => w.FkCalling)
                .SingleOrDefaultAsync(m => m.WardMemberId == id);
            if (wardMember == null)
            {
                return NotFound();
            }

            return View(wardMember);
        }

        // GET: WardMembers/Create
        public IActionResult Create()
        {
            ViewData["FkCallingId"] = new SelectList(_context.Calling, "CallingId", "CallingName");
            return View();
        }

        // POST: WardMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WardMemberId,Fname,Lname,FkCallingId")] WardMember wardMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wardMember);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FkCallingId"] = new SelectList(_context.Calling, "CallingId", "CallingName", wardMember.FkCallingId);
            return View(wardMember);
        }

        // GET: WardMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardMember = await _context.WardMember.SingleOrDefaultAsync(m => m.WardMemberId == id);
            if (wardMember == null)
            {
                return NotFound();
            }
            ViewData["FkCallingId"] = new SelectList(_context.Calling, "CallingId", "CallingName", wardMember.FkCallingId);
            return View(wardMember);
        }

        // POST: WardMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WardMemberId,Fname,Lname,FkCallingId")] WardMember wardMember)
        {
            if (id != wardMember.WardMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wardMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WardMemberExists(wardMember.WardMemberId))
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
            ViewData["FkCallingId"] = new SelectList(_context.Calling, "CallingId", "CallingName", wardMember.FkCallingId);
            return View(wardMember);
        }

        // GET: WardMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardMember = await _context.WardMember
                .Include(w => w.FkCalling)
                .SingleOrDefaultAsync(m => m.WardMemberId == id);
            if (wardMember == null)
            {
                return NotFound();
            }

            return View(wardMember);
        }

        // POST: WardMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wardMember = await _context.WardMember.SingleOrDefaultAsync(m => m.WardMemberId == id);
            _context.WardMember.Remove(wardMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WardMemberExists(int id)
        {
            return _context.WardMember.Any(e => e.WardMemberId == id);
        }
    }
}
