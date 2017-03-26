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
    public class SpeakersController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SpeakersController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.Speaker.Include(s => s.FkSpeakerTypeNavigation).Include(s => s.FkTopicNavigation).Include(s => s.FkWardMemberNavigation);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.FkSpeakerTypeNavigation)
                .Include(s => s.FkTopicNavigation)
                .Include(s => s.FkWardMemberNavigation)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["FkSpeakerType"] = new SelectList(_context.SpeakerType, "SpeakerTypeId", "SpeakerType1");
            ViewData["FkTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");
            ViewData["FkWardMember"] = new SelectList(_context.WardMember, "WardMemberId", "Fname");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,SpeakerDate,FkWardMember,FkTopic,FkSpeakerType")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FkSpeakerType"] = new SelectList(_context.SpeakerType, "SpeakerTypeId", "SpeakerType1", speaker.FkSpeakerType);
            ViewData["FkTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.FkTopic);
            ViewData["FkWardMember"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", speaker.FkWardMember);
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }
            ViewData["FkSpeakerType"] = new SelectList(_context.SpeakerType, "SpeakerTypeId", "SpeakerType1", speaker.FkSpeakerType);
            ViewData["FkTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.FkTopic);
            ViewData["FkWardMember"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", speaker.FkWardMember);
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,SpeakerDate,FkWardMember,FkTopic,FkSpeakerType")] Speaker speaker)
        {
            if (id != speaker.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.SpeakerId))
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
            ViewData["FkSpeakerType"] = new SelectList(_context.SpeakerType, "SpeakerTypeId", "SpeakerType1", speaker.FkSpeakerType);
            ViewData["FkTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", speaker.FkTopic);
            ViewData["FkWardMember"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", speaker.FkWardMember);
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.FkSpeakerTypeNavigation)
                .Include(s => s.FkTopicNavigation)
                .Include(s => s.FkWardMemberNavigation)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            _context.Speaker.Remove(speaker);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speaker.Any(e => e.SpeakerId == id);
        }
    }
}
