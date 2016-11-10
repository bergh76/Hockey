using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockey.Data;
using Hockey.Models;
using Microsoft.AspNetCore.Authorization;
using Hockey.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;


namespace Hockey.Areas.Admin.Controllers
{

    public class PlayersController : Controller
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

        public PlayersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            //_file = file;

        }

        // GET: Players
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var playerList = from p in _context.Player
                                 join c in _context.Conference on p.ConferenceId equals c.ConferenceId
                                 join d in _context.Division on p.DivisionId equals d.DivisionId
                                 join t in _context.Team on p.TeamId equals t.TeamId
                                 join y in _context.Position on p.PositionId equals y.PositionId
                                 join i in _context.Image on p.ImageId equals i.ImageId
                                 join n in _context.Nationality on p.NationalityId equals n.NationalityId
                                 join ti in _context.TeamImage on p.TeamImageId equals ti.TeamImageId
                                 join s in _context.Season on p.SeasonId equals s.SeasonId
                                 join m in _context.CardManufacture on p.CardManufactureId equals m.CardManufactureId
                                 //join l in _context.League on p.LeagueId equals l.LeagueId
                                 select new NhlPlayerViewModel
                                 {
                                     CardNumber = p.PlayerCardId,
                                     NationalityImgPath = n.NationalityImageName + n.NationalityImagePath,
                                     Nationality = n.NationalityName,
                                     FirstName = p.PlayerFirstName,
                                     LastName = p.PlayerLastName,
                                     Position = y.PositionShortName,
                                     JersyNumber = p.PlayerJersyNumber,
                                     //League = l.LeagueName,
                                     Conference = c.ConferenceName,
                                     Division = d.DivisionName,
                                     TeamImgPath = ti.TeamImageName + ti.TeamImagePath,
                                     Team = t.TeamName,
                                     CardManufacture = m.MakerName,
                                     Season = s.SeasonName,
                                     ImagePath = i.ImageName + i.ImagePath,
                                     ISSigned = p.ISSigned,
                                     PlayerId = p.PlayerId
                                 };
                IEnumerable<NhlPlayerViewModel> pwModel = await playerList.ToListAsync();
                return View(pwModel);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
                //return View();

           
        }

        // GET: Players/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }


        // GET: Players/Create
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                ViewData["CardManufacture"] = new SelectList(_context.CardManufacture.OrderBy(x => x.MakerName), "CardManufactureId", "MakerName");
                ViewData["League"] = new SelectList(_context.League.OrderBy(x => x.LeagueName), "LeagueId", "LeagueName");
                ViewData["Team"] = new SelectList(_context.Team.OrderBy(x => x.TeamName), "TeamId", "TeamName");
                ViewData["Division"] = new SelectList(_context.Division.OrderBy(x => x.DivisionName), "DivisionId", "DivisionName");
                ViewData["Conference"] = new SelectList(_context.Conference.OrderBy(x => x.ConferenceName), "ConferenceId", "ConferenceName");
                ViewData["Season"] = new SelectList(_context.Season.OrderByDescending(x => x.SeasonName), "SeasonId", "SeasonName");
                ViewData["Position"] = new SelectList(_context.Position.OrderBy(x => x.PositionId), "PositionId", "PositionType");
                ViewData["Nationality"] = new SelectList(_context.Nationality.OrderBy(x => x.NationalityId), "NationalityId", "NationalityName");
            }
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Create([Bind("PlayerCardId, PositionId,PlayerFirstName,PlayerLastName,PlayerJersyNumber, PlayerImage,ISActive,ISSigned,Value,ConferenceId,DivisionId,SeasonId,NhlTeamId,ImageId,CardManufactureId,LeaugeId,PlayerAddDate,TeamImageId,NationalityId")] Player players)
        {

            if (ModelState.IsValid)
            {
                _context.Add(players);
                //create filename
                var fileName = string.Format("{0}_{1}_{2}",players.PlayerCardId, players.PlayerFirstName, players.PlayerLastName) + _fileExtension;
                string league = await _context.League.Where(x => x.LeagueId == players.LeagueId).Select(x => x.LeagueName).FirstOrDefaultAsync();
                string team = await _context.Team.Where(x => x.TeamId == players.TeamId).Select(x => x.TeamName).FirstOrDefaultAsync();
                string year = await _context.Season.Where(x => x.SeasonId == players.SeasonId).Select(x => x.SeasonName).FirstOrDefaultAsync();
                string position = await _context.Position.Where(x => x.PositionId == players.PositionId).Select(x => x.PositionShortName).FirstOrDefaultAsync();
                _context.SaveChanges();

                //To method to ad Image
                NewImage(players, fileName, team, year, position, league);
                players.ImageId = await _context.Image.Select(x => x.ImageId).LastOrDefaultAsync();
                await _context.SaveChangesAsync();
                return View();
            }
            return View(players);
        }

        public Image NewImage(Player players, string fileName,string leauge, string team, string year, string position)
        {
            _root = _hostEnvironment.WebRootPath;
            string imgPath = string.Format("images/{0}/{1}/{2}/{3}/{4}_{5}",leauge, team, year, position, players.PlayerFirstName, players.PlayerLastName);
            var uploads = _root + "/" + imgPath;
            Directory.CreateDirectory(uploads);
            var img = new Image
            {
                ImageName = fileName,
                ImagePath = imgPath,
                PlayerId = players.PlayerId
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

       

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerCardId, PositionId,PlayerFirstName,PlayerLastName,PlayerJersyNumber, PlayerImage,ISActive,ISSigned,Value,ConferenceId,DivisionId,SeasonId,NhlTeamId,ImageId,LeaugeId,CardManufactureId,PlayerAddDate,TeamImageId,NationalityId")] Player players)
        {
            ViewData["Maker"] = new SelectList(_context.CardManufacture.OrderBy(x => x.MakerName), "Id", "MakerName");
            ViewData["Team"] = new SelectList(_context.Team.OrderBy(x => x.TeamName), "Id", "TeamName");
            ViewData["Division"] = new SelectList(_context.Division.OrderBy(x => x.DivisionName), "Id", "DivisionName");
            ViewData["Conference"] = new SelectList(_context.Conference.OrderBy(x => x.ConferenceName), "Id", "ConferenceName");
            ViewData["Position"] = new SelectList(_context.Position.OrderBy(x => x.PositionShortName), "Id", "Position");

            if (id != players.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.PlayerId))
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
            return View(players);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Player.SingleOrDefaultAsync(m => m.PlayerId == id);
            _context.Player.Remove(players);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlayersExists(int id)
        {
            return _context.Player.Any(e => e.PlayerId == id);
        }
    }
}
