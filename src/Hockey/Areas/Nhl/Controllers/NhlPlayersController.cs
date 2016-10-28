using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockey.Data;
using Hockey.Models;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.IO;
using Hockey.HelperClasses;

namespace Hockey.Areas.Nhl.Controllers
{
    public class NhlPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment; // service that provides some useful environment information such as the current file path
        //private IFormFile _file;
        private static string _root;
        public static System.Drawing.Image _imageFile;
        public NhlPlayersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            //_file = file;

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
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "MakerName");
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceName");
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionName");
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityName");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionType");
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonName");
            ViewData["TeamId"] = new SelectList(_context.Team.Where(x => x.LeagueId == 1).ToList(), "TeamId", "TeamName");
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
                string ext = ImageFromDOMHelper._fileExtension;
                var fileName = string.Format("{0}_{1}_{2}.", nhlPlayer.NhlPlayerCardId, nhlPlayer.PlayerFirstName, nhlPlayer.PlayerLastName) + ext;
                ImageCreator._fileName = fileName;
                string league = await _context.League.Where(x => x.LeagueId == 1).Select(x => x.LeagueName).FirstOrDefaultAsync();
                string team = await _context.Team.Where(x => x.TeamId == nhlPlayer.TeamId).Select(x => x.TeamName).FirstOrDefaultAsync();
                string year = await _context.Season.Where(x => x.SeasonId == nhlPlayer.SeasonId).Select(x => x.SeasonName).FirstOrDefaultAsync();
                string position = await _context.Position.Where(x => x.PositionId == nhlPlayer.PositionId).Select(x => x.PositionType).FirstOrDefaultAsync();
                NewImage(nhlPlayer, fileName, team, year, position, league);
                await _context.SaveChangesAsync();
                //To method to ad Image

                nhlPlayer.PlayerAddDate = DateTime.Now;
                nhlPlayer.ImageId = await _context.Image.Select(x => x.ImageId).LastOrDefaultAsync();

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            //ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            //ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            //ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            //ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            //ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            //ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            return View(nhlPlayer);
        }

        public Image NewImage(NhlPlayer nhlPlayer, string fileName, string leauge, string team, string year, string position)
        {
            _root = _hostEnvironment.WebRootPath;
            string imgPath = string.Format("images/{0}/{1}/{2}/{3}/{4}_{5}", leauge, team, year, position, nhlPlayer.PlayerFirstName, nhlPlayer.PlayerLastName);
            var uploads = _root + "/" + imgPath;
            ImageCreator._uploads = uploads;
            Directory.CreateDirectory(uploads);
            var img = new Image
            {
                ImageName = fileName,
                ImagePath = imgPath,
                PlayerId = nhlPlayer.NhlPlayerId
            };
            var createImage = new ImageCreator();
            createImage.ImageCreate();
            _context.Add(img);
            _context.SaveChanges();
            return img;
        }


        [HttpPost]
        public string ImageData(string imageData)
        {
            var data = new ImageFromDOMHelper();
            data.ImageData(imageData);
            return imageData;
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
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "LeagueId", nhlPlayer.LeagueId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
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
