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
    public class SacramentMeetingsController : Controller
    {
        private readonly SacramentPlannerContext _context;

        public SacramentMeetingsController(SacramentPlannerContext context)
        {
            _context = context;    
        }

        // GET: SacramentMeetings
        public async Task<IActionResult> Index()
        {
            var sacramentPlannerContext = _context.SacramentMeeting.Include(s => s.FkClosingPrayerNavigation).Include(s => s.FkClosingSongNavigation).Include(s => s.FkConductingNavigation).Include(s => s.FkIntermediateSongNavigation).Include(s => s.FkMeetingTopicNavigation).Include(s => s.FkMusicLeaderNavigation).Include(s => s.FkMusicPlayerNavigation).Include(s => s.FkOpenPrayerNavigation).Include(s => s.FkOpenSongNavigation).Include(s => s.FkSacramentSongNavigation);
            return View(await sacramentPlannerContext.ToListAsync());
        }

        // GET: SacramentMeetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentMeeting = await _context.SacramentMeeting
                .Include(s => s.FkClosingPrayerNavigation)
                .Include(s => s.FkClosingSongNavigation)
                .Include(s => s.FkConductingNavigation)
                .Include(s => s.FkIntermediateSongNavigation)
                .Include(s => s.FkMeetingTopicNavigation)
                .Include(s => s.FkMusicLeaderNavigation)
                .Include(s => s.FkMusicPlayerNavigation)
                .Include(s => s.FkOpenPrayerNavigation)
                .Include(s => s.FkOpenSongNavigation)
                .Include(s => s.FkSacramentSongNavigation)
                .SingleOrDefaultAsync(m => m.SacramentMeetingId == id);
            if (sacramentMeeting == null)
            {
                return NotFound();
            }

            return View(sacramentMeeting);
        }

        // GET: SacramentMeetings/Create
        public IActionResult Create()
        {
            ViewData["FkClosingPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId");
            ViewData["FkClosingSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle");
            ViewData["FkConducting"] = new SelectList(_context.WardMember, "WardMemberId", "Fname");
            ViewData["FkIntermediateSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle");
            ViewData["FkMeetingTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle");
            ViewData["FkMusicLeader"] = new SelectList(_context.WardMember, "WardMemberId", "Fname");
            ViewData["FkMusicPlayer"] = new SelectList(_context.WardMember, "WardMemberId", "Fname");
            ViewData["FkOpenPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId");
            ViewData["FkOpenSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle");
            ViewData["FkSacramentSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle");
            return View();
        }

        // POST: SacramentMeetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SacramentMeetingId,SacramentDate,FkConducting,FkMusicLeader,FkMusicPlayer,FkMeetingTopic,BabyBlessing,Confirmation,FkOpenPrayer,FkOpenSong,FkSacramentSong,FkWardBusiness,FkYouthSpeaker,FkFirstSpeaker,FkSecondSpeaker,FkIntermediateSong,FkClosingSong,FkClosingPrayer")] SacramentMeeting sacramentMeeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sacramentMeeting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FkClosingPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkClosingPrayer);
            ViewData["FkClosingSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkClosingSong);
            ViewData["FkConducting"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkConducting);
            ViewData["FkIntermediateSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkIntermediateSong);
            ViewData["FkMeetingTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", sacramentMeeting.FkMeetingTopic);
            ViewData["FkMusicLeader"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicLeader);
            ViewData["FkMusicPlayer"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicPlayer);
            ViewData["FkOpenPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkOpenPrayer);
            ViewData["FkOpenSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkOpenSong);
            ViewData["FkSacramentSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkSacramentSong);
            return View(sacramentMeeting);
        }

        // GET: SacramentMeetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentMeeting = await _context.SacramentMeeting.SingleOrDefaultAsync(m => m.SacramentMeetingId == id);
            if (sacramentMeeting == null)
            {
                return NotFound();
            }
            ViewData["FkClosingPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkClosingPrayer);
            ViewData["FkClosingSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkClosingSong);
            ViewData["FkConducting"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkConducting);
            ViewData["FkIntermediateSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkIntermediateSong);
            ViewData["FkMeetingTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", sacramentMeeting.FkMeetingTopic);
            ViewData["FkMusicLeader"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicLeader);
            ViewData["FkMusicPlayer"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicPlayer);
            ViewData["FkOpenPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkOpenPrayer);
            ViewData["FkOpenSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkOpenSong);
            ViewData["FkSacramentSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkSacramentSong);
            return View(sacramentMeeting);
        }

        // POST: SacramentMeetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SacramentMeetingId,SacramentDate,FkConducting,FkMusicLeader,FkMusicPlayer,FkMeetingTopic,BabyBlessing,Confirmation,FkOpenPrayer,FkOpenSong,FkSacramentSong,FkWardBusiness,FkYouthSpeaker,FkFirstSpeaker,FkSecondSpeaker,FkIntermediateSong,FkClosingSong,FkClosingPrayer")] SacramentMeeting sacramentMeeting)
        {
            if (id != sacramentMeeting.SacramentMeetingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sacramentMeeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentMeetingExists(sacramentMeeting.SacramentMeetingId))
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
            ViewData["FkClosingPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkClosingPrayer);
            ViewData["FkClosingSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkClosingSong);
            ViewData["FkConducting"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkConducting);
            ViewData["FkIntermediateSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkIntermediateSong);
            ViewData["FkMeetingTopic"] = new SelectList(_context.Topic, "TopicId", "TopicTitle", sacramentMeeting.FkMeetingTopic);
            ViewData["FkMusicLeader"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicLeader);
            ViewData["FkMusicPlayer"] = new SelectList(_context.WardMember, "WardMemberId", "Fname", sacramentMeeting.FkMusicPlayer);
            ViewData["FkOpenPrayer"] = new SelectList(_context.Prayer, "PrayerId", "PrayerId", sacramentMeeting.FkOpenPrayer);
            ViewData["FkOpenSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkOpenSong);
            ViewData["FkSacramentSong"] = new SelectList(_context.Hymn, "HymnId", "HymnTitle", sacramentMeeting.FkSacramentSong);
            return View(sacramentMeeting);
        }

        // GET: SacramentMeetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacramentMeeting = await _context.SacramentMeeting
                .Include(s => s.FkClosingPrayerNavigation)
                .Include(s => s.FkClosingSongNavigation)
                .Include(s => s.FkConductingNavigation)
                .Include(s => s.FkIntermediateSongNavigation)
                .Include(s => s.FkMeetingTopicNavigation)
                .Include(s => s.FkMusicLeaderNavigation)
                .Include(s => s.FkMusicPlayerNavigation)
                .Include(s => s.FkOpenPrayerNavigation)
                .Include(s => s.FkOpenSongNavigation)
                .Include(s => s.FkSacramentSongNavigation)
                .SingleOrDefaultAsync(m => m.SacramentMeetingId == id);
            if (sacramentMeeting == null)
            {
                return NotFound();
            }

            return View(sacramentMeeting);
        }

        // POST: SacramentMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacramentMeeting = await _context.SacramentMeeting.SingleOrDefaultAsync(m => m.SacramentMeetingId == id);
            _context.SacramentMeeting.Remove(sacramentMeeting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SacramentMeetingExists(int id)
        {
            return _context.SacramentMeeting.Any(e => e.SacramentMeetingId == id);
        }
    }
}
