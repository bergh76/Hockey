using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockey.Data;
using Hockey.Models;

namespace Hockey.Areas.Shl.Controllers
{
    public class ShlPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShlPlayersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ShlPlayers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShlPlayer.Include(s => s._CardManufacture).Include(s => s._Image).Include(s => s._Leauge).Include(s => s._Nationality).Include(s => s._Position).Include(s => s._Season).Include(s => s._Team).Include(s => s._TeamImage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShlPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shlPlayer = await _context.ShlPlayer.SingleOrDefaultAsync(m => m.ShlPlayerId == id);
            if (shlPlayer == null)
            {
                return NotFound();
            }

            return View(shlPlayer);
        }

        // GET: ShlPlayers/Create
        public IActionResult Create()
        {
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId");
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId");
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId");
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId");
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId");
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId");
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId");
            return View();
        }

        // POST: ShlPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShlPlayerId,CardManufactureId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,PlayerAddDate,PlayerCardId,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] ShlPlayer shlPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shlPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", shlPlayer.CardManufactureId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", shlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", shlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", shlPlayer.TeamImageId);
            return View(shlPlayer);
        }

        // GET: ShlPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shlPlayer = await _context.ShlPlayer.SingleOrDefaultAsync(m => m.ShlPlayerId == id);
            if (shlPlayer == null)
            {
                return NotFound();
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", shlPlayer.CardManufactureId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", shlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", shlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", shlPlayer.TeamImageId);
            return View(shlPlayer);
        }

        // POST: ShlPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShlPlayerId,CardManufactureId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,PlayerAddDate,PlayerCardId,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] ShlPlayer shlPlayer)
        {
            if (id != shlPlayer.ShlPlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shlPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShlPlayerExists(shlPlayer.ShlPlayerId))
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
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", shlPlayer.CardManufactureId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", shlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", shlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", shlPlayer.TeamImageId);
            return View(shlPlayer);
        }

        // GET: ShlPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shlPlayer = await _context.ShlPlayer.SingleOrDefaultAsync(m => m.ShlPlayerId == id);
            if (shlPlayer == null)
            {
                return NotFound();
            }

            return View(shlPlayer);
        }

        // POST: ShlPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shlPlayer = await _context.ShlPlayer.SingleOrDefaultAsync(m => m.ShlPlayerId == id);
            _context.ShlPlayer.Remove(shlPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ShlPlayerExists(int id)
        {
            return _context.ShlPlayer.Any(e => e.ShlPlayerId == id);
        }
    }
}
