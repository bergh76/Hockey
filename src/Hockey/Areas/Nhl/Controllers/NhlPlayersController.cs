using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockey.Data;
using Hockey.Models;

namespace Hockey.Areas.Nhl.Controllers
{
    public class NhlPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhlPlayersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: NhlPlayers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NhlPlayer
                .Include(n => n._CardManufacture)
                .Include(n => n._Conference)
                .Include(n => n._Division)
                .Include(n => n._Image)
                .Include(n => n._Leauge)
                .Include(n => n._Nationality)
                .Include(n => n._Position)
                .Include(n => n._Season)
                .Include(n => n._Team)
                .Include(n => n._TeamImage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NhlPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }

            return View(nhlPlayer);
        }

        // GET: NhlPlayers/Create
        public IActionResult Create()
        {
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId");
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId");
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId");
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId");
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId");
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId");
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId");
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId");
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId");
            return View();
        }

        // POST: NhlPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhlPlayerId,CardManufactureId,ConferenceId,DivisionId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,NhlPlayerCardId,PlayerAddDate,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] NhlPlayer nhlPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhlPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", nhlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", nhlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", nhlPlayer.TeamImageId);
            return View(nhlPlayer);
        }

        // GET: NhlPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", nhlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", nhlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", nhlPlayer.TeamImageId);
            return View(nhlPlayer);
        }

        // POST: NhlPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhlPlayerId,CardManufactureId,ConferenceId,DivisionId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,NhlPlayerCardId,PlayerAddDate,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] NhlPlayer nhlPlayer)
        {
            if (id != nhlPlayer.NhlPlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhlPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhlPlayerExists(nhlPlayer.NhlPlayerId))
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
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            ViewData["ImageId"] = new SelectList(_context.Image, "ImageId", "ImageId", nhlPlayer.ImageId);
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", nhlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            ViewData["TeamImageId"] = new SelectList(_context.TeamImage, "TeamImageId", "TeamImageId", nhlPlayer.TeamImageId);
            return View(nhlPlayer);
        }

        // GET: NhlPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }

            return View(nhlPlayer);
        }

        // POST: NhlPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            _context.NhlPlayer.Remove(nhlPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NhlPlayerExists(int id)
        {
            return _context.NhlPlayer.Any(e => e.NhlPlayerId == id);
        }
    }
}
