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

namespace Hockey.Areas.Shl.Controllers
{
    public class ShlPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment; // service that provides some useful environment information such as the current file path
        //private IFormFile _file;
        private static string _root;
        public static string _imageData;
        public static string _fileExtension;
        public static Match _imageMatch;
        public static string _mimeType;
        public static System.Drawing.Image _imageFile;
        public ShlPlayersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            //_file = file;

        }

        // GET: ShlPlayers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShlPlayer
                .Include(s => s._CardManufacture)
                .Include(s => s._Image)
                .Include(s => s._Leauge)
                .Include(s => s._Nationality)
                .Include(s => s._Position)
                .Include(s => s._Season)
                .Include(s => s._Team)
                .Include(s => s._TeamImage);
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
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "MakerName");
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityName");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionShortName");
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonName");
            ViewData["TeamId"] = new SelectList(_context.Team.Where(x => x.LeagueId == 2).ToList(),"TeamId","TeamName");//.Select(x => x.TeamName));
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
                var fileName = string.Format("{0}_{1}_{2}", shlPlayer.PlayerCardId, shlPlayer.PlayerFirstName, shlPlayer.PlayerLastName) + _fileExtension;
                string league = await _context.League.Where(x => x.LeagueId == shlPlayer.LeagueId).Select(x => x.LeagueName).FirstOrDefaultAsync();
                string team = await _context.Team.Where(x => x.TeamId == shlPlayer.TeamId).Select(x => x.TeamName).FirstOrDefaultAsync();
                string year = await _context.Season.Where(x => x.SeasonId == shlPlayer.SeasonId).Select(x => x.SeasonName).FirstOrDefaultAsync();
                string position = await _context.Position.Where(x => x.PositionId == shlPlayer.PositionId).Select(x => x.PositionShortName).FirstOrDefaultAsync();
                _context.SaveChanges();

                //To method to ad Image
                NewImage(shlPlayer, fileName, team, year, position, league);
                shlPlayer.PlayerAddDate = DateTime.Now;
                shlPlayer.ImageId = await _context.Image.Select(x => x.ImageId).LastOrDefaultAsync();
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", shlPlayer.CardManufactureId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
            return View(shlPlayer);
        }
        public Image NewImage(ShlPlayer shlPlayer, string fileName, string leauge, string team, string year, string position)
        {
            _root = _hostEnvironment.WebRootPath;
            string imgPath = string.Format("images/{0}/{1}/{2}/{3}/{4}_{5}", leauge, team, year, position, shlPlayer.PlayerFirstName, shlPlayer.PlayerLastName);
            var uploads = _root + "/" + imgPath;
            Directory.CreateDirectory(uploads);
            var img = new Image
            {
                ImageName = fileName,
                ImagePath = imgPath,
                PlayerId = shlPlayer.ShlPlayerId
            };
            UploadImage(fileName, uploads);
            _context.Add(img);
            _context.SaveChanges();
            return img;
        }
        public void UploadImage(string fileName, string uploads)
        {
            // TODO: Fix a Image Bulder to create an Imager .png/.jpg or other format from Base64StringFormat
            if (string.IsNullOrEmpty(_imageData))
                throw new ArgumentNullException(nameof(_imageData), "No image data received");

            _imageMatch = Regex.Match(_imageData, @"^data:(?<mimetype>[^;]+);base64,(?<data>.+)$");
            if (!_imageMatch.Success)
                throw new ArgumentException("imageData is in unknown format", nameof(_imageData));

            _mimeType = _imageMatch.Groups["mimetype"].Value;
            Match imageType = Regex.Match(_mimeType, @"^[^/]+/(?<type>.+?)$");
            if (!imageType.Success)
                throw new ArgumentException($"mimeType format invalid for {_mimeType}", nameof(_mimeType));

            _fileExtension = imageType.Groups["type"].Value;
            byte[] data = Convert.FromBase64String(_imageMatch.Groups["data"].Value);
            var bytes = data;
            using (var imageFile = new FileStream(_imageData, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    imageFile.CopyToAsync(fileStream);
                }
                imageFile.Flush();
            }

        }

        [HttpPost]
        public string ImageData(string imageData)
        {
            if (string.IsNullOrEmpty(imageData))
                throw new ArgumentNullException(nameof(imageData), "No image data received");

            Match imageMatch = Regex.Match(imageData, @"^data:(?<mimetype>[^;]+);base64,(?<data>.+)$");
            if (!imageMatch.Success)
                throw new ArgumentException("imageData is in unknown format", nameof(imageData));

            string mimeType = imageMatch.Groups["mimetype"].Value;
            Match imageType = Regex.Match(mimeType, @"^[^/]+/(?<type>.+?)$");
            if (!imageType.Success)
                throw new ArgumentException($"mimeType format invalid for {mimeType}", nameof(mimeType));

            string fileExtension = imageType.Groups["type"].Value;
            byte[] data = Convert.FromBase64String(imageMatch.Groups["data"].Value);

            _imageData = imageData;
            _fileExtension = fileExtension;
            _imageMatch = imageMatch;
            _mimeType = mimeType;

            return imageData;
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
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
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
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", shlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", shlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", shlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", shlPlayer.TeamId);
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
